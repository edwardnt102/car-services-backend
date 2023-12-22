﻿using AutoMapper;
using Common;
using Common.Constants;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repository;
using Serilog;
using Services.Interfaces;
using Services.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class PlacesServices : IPlacesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public PlacesServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices,
            IOptions<AppSettings> options, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _mapper = mapper;
            _appSettings = options.Value;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IPagedResult<bool>> DeleteAsync(long id)
        {
            if (id == 0)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.IdNotFound,
                    Data = false
                };
            }
            try
            {
                await _unitOfWork.OpenTransaction();
                var place = await _unitOfWork.PlacesRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (place != null && place.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", place.Id);
                    p.Add("@Title", place.Title);
                    p.Add("@TableName", "Places");
                    var checkExists = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("spCheckExistsWhenDelete", p);
                    if (checkExists?.FirstOrDefault()?.Value != string.Empty)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = checkExists?.FirstOrDefault()?.Value,
                            Data = false
                        };
                    }
                    place.IsDeleted = true;
                    _unitOfWork.PlacesRepository.Update(place);
                    await _unitOfWork.PlacesRepository.CommitAsync().ConfigureAwait(false);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.DeleteSuccess,
                        Data = true
                    };
                }
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PlacesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<PlaceViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                request ??= new PagingRequest
                {
                    Page = 1,
                    PageSize = 10,
                    SortDir = Constants.SortDesc,
                    SortField = Constants.SortId
                };
                var p = new DynamicParameters();
                p.Add(Constants.Key, request.SearchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstPlaces = await _dapperRepository.QueryMultipleUsingStoreProcAsync<PlaceViewModel>("uspPlaces_selectAll", p);
                var placeViewModels = lstPlaces.ToList();

                var total = placeViewModels.Count;
                lstPlaces = !string.IsNullOrEmpty(request.SortField) ? placeViewModels.OrderBy(request) : placeViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstPlaces = lstPlaces.Paging(request);
                }

                if (null == lstPlaces)
                {
                    return new PagedResults<PlaceViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstPlaces.ToList();
                viewModels.ForEach(x =>
                {
                    var listAttachmentReName = new List<string>();
                    if (!string.IsNullOrEmpty(x.AttachmentFileReName))
                    {
                        var list = x.AttachmentFileReName.Split(Constants.Semicolon);
                        listAttachmentReName.AddRange(list.Select(attachmentFileReName => _appSettings.DomainFile + attachmentFileReName));
                    }
                    x.ListAttachmentReName = listAttachmentReName;

                    var listAttachmentOriginalName = new List<string>();
                    if (!string.IsNullOrEmpty(x.AttachmentFileOriginalName))
                    {
                        var list = x.AttachmentFileOriginalName.Split(Constants.Semicolon);
                        listAttachmentOriginalName.AddRange(list.Select(attachmentFileOriginalName => attachmentFileOriginalName));
                    }
                    x.ListAttachmentOriginalName = listAttachmentOriginalName;
                    x.LocationName = !string.IsNullOrEmpty(x.WardName) ? x.WardName : !string.IsNullOrEmpty(x.StreetName) ? x.StreetName : x.ProjectName;
                });

                return new PagedResults<PlaceViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PlacesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstPlaces = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspPlaces_Suggestion", p);

                if (null == lstPlaces)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstPlaces.ToList();
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = itemModels,
                    TotalRecords = itemModels.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PlacesServices: " + ex.Message);
                throw;
            }
        }


        public async Task<IPagedResult<bool>> SaveAsync(Places entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var place = await _unitOfWork.PlacesRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (place != null && place.Id > 0)
                {
                    var fileNameOld = place.AttachmentFileReName ?? string.Empty;
                    place.AttachmentFileReName = attachReName;
                    place.AttachmentFileOriginalName = attachOriginalName;
                    place.History = entity.History;
                    place.Chat = entity.Chat;
                    place.Subtitle = entity.Subtitle;
                    place.Description = entity.Description;
                    place.PriceId = entity.PriceId;
                    place.ProvinceId = entity.ProvinceId;
                    place.DistrictId = entity.DistrictId;
                    place.WardId = entity.WardId;
                    place.Address = entity.Address;
                    place.Longitude = entity.Longitude;
                    place.Latitude = entity.Latitude;
                    place.RuleId = entity.RuleId;
                    place.Title = entity.Title;

                    _unitOfWork.PlacesRepository.Update(place);
                    var update = await _unitOfWork.PlacesRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                    }
                }
                else
                {
                    var newModel = _mapper.Map<Places>(entity);
                    newModel.AttachmentFileReName = attachReName;
                    newModel.AttachmentFileOriginalName = attachOriginalName;

                    _unitOfWork.PlacesRepository.Add(newModel);
                    var insert = await _unitOfWork.PlacesRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
                    }
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PlacesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
