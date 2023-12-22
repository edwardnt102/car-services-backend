using Common;
using Common.Constants;
using Common.Pagging;
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
    public class CarModelsServices : ICarModelsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;

        public CarModelsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices, IOptions<AppSettings> options)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _appSettings = options.Value;
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
                var carModel = await _unitOfWork.CarModelsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (carModel != null && carModel.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", carModel.Id);
                    p.Add("@Title", carModel.Title);
                    p.Add("@TableName", "CarModels");
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
                    carModel.IsDeleted = true;
                    _unitOfWork.CarModelsRepository.Update(carModel);
                    await _unitOfWork.CarModelsRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarModelsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<CarModelViewModel>> GetAllAsync(PagingRequest request)
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
                var lstCarModels = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CarModelViewModel>("uspCarModels_selectAll", p);
                var carModelViewModels = lstCarModels.ToList();

                var total = carModelViewModels.Count;
                lstCarModels = !string.IsNullOrEmpty(request.SortField) ? carModelViewModels.OrderBy(request) : carModelViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstCarModels = lstCarModels.Paging(request);
                }

                if (null == lstCarModels)
                {
                    return new PagedResults<CarModelViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var modelViewModels = lstCarModels.ToList();
                modelViewModels.ForEach(x =>
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

                    var listModelImage = new List<string>();
                    if (!string.IsNullOrEmpty(x.ModelImage))
                    {
                        var list = x.ModelImage.Split(Constants.Semicolon);
                        listModelImage.AddRange(list.Select(modelImage => _appSettings.DomainFile + modelImage));
                    }
                    x.ListModelImage = listModelImage;

                });

                return new PagedResults<CarModelViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = modelViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarModelsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstCarModels = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspCarModels_Suggestion", p);

                if (null == lstCarModels)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstCarModels.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarModelsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(CarModels entity, List<IFormFile> files, List<IFormFile> modelImage)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var carModel = await _unitOfWork.CarModelsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                var modelImageReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(modelImage);
                if (carModel != null && carModel.Id > 0)
                {
                    var fileNameOld = carModel.AttachmentFileReName ?? string.Empty;
                    var modelImageOld = carModel.ModelImage ?? string.Empty;
                    carModel.History = entity.History;
                    carModel.Chat = entity.Chat;
                    carModel.Subtitle = entity.Subtitle;
                    carModel.Description = entity.Description;
                    carModel.BrandId = entity.BrandId;
                    carModel.ClassId = entity.ClassId;
                    carModel.Note = entity.Note;
                    carModel.Long = entity.Long;
                    carModel.High = entity.High;
                    carModel.Heavy = entity.Heavy;
                    carModel.ReferencePrice = entity.ReferencePrice;
                    carModel.AttachmentFileReName = attachReName;
                    carModel.AttachmentFileOriginalName = attachOriginalName;
                    carModel.Title = entity.Title;
                    carModel.Width = entity.Width;
                    carModel.ModelImage = modelImageReName;
                    _unitOfWork.CarModelsRepository.Update(carModel);
                    var update = await _unitOfWork.CarModelsRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                        _uploadFileServices.UploadMultipleFileAsync(modelImage, modelImageReName, modelImageOld);
                    }
                }
                else
                {
                    var model = new CarModels
                    {
                        History = entity.History,
                        Chat = entity.Chat,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        BrandId = entity.BrandId,
                        ClassId = entity.ClassId,
                        Note = entity.Note,
                        Long = entity.Long,
                        High = entity.High,
                        Heavy = entity.Heavy,
                        ReferencePrice = entity.ReferencePrice,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        Title = entity.Title,
                        Width = entity.Width,
                        ModelImage = modelImageReName
                    };
                    _unitOfWork.CarModelsRepository.Add(model);
                    var insert = await _unitOfWork.CarModelsRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
                        _uploadFileServices.UploadMultipleFileAsync(modelImage, modelImageReName, string.Empty);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarModelsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
