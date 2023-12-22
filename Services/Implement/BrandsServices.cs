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
    public class BrandsServices : IBrandsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;

        public BrandsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices, IOptions<AppSettings> options)
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
                var brand = await _unitOfWork.BrandsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (brand != null && brand.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", brand.Id);
                    p.Add("@Title", brand.Title);
                    p.Add("@TableName", "Brands");
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
                    brand.IsDeleted = true;
                    _unitOfWork.BrandsRepository.Update(brand);
                    await _unitOfWork.BrandsRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BrandsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<BrandViewModel>> GetAllAsync(PagingRequest request)
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
                var lstBrands = await _dapperRepository.QueryMultipleUsingStoreProcAsync<BrandViewModel>("uspBrands_selectAll", p);
                var brandsEnumerable = lstBrands.ToList();

                var total = brandsEnumerable.Count;
                lstBrands = !string.IsNullOrEmpty(request.SortField) ? brandsEnumerable.OrderBy(request) : brandsEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstBrands = lstBrands.Paging(request);
                }

                if (null == lstBrands)
                {
                    return new PagedResults<BrandViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var brandViewModels = lstBrands.ToList();
                brandViewModels.ForEach(x =>
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

                    x.Logo = !string.IsNullOrEmpty(x.Logo) ? _appSettings.DomainFile + x.Logo : x.Logo;
                });

                return new PagedResults<BrandViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = brandViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BrandsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(Brands entity, List<IFormFile> files, IFormFile logo)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var brand = await _unitOfWork.BrandsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                var logos = _uploadFileServices.GetFileNameSingleFileReNameAsync(logo);
                if (brand != null && brand.Id > 0)
                {
                    var fileNameOld = brand.AttachmentFileReName ?? string.Empty;
                    var logoOld = brand.Logo ?? string.Empty;
                    brand.Title = entity.Title;
                    brand.Subtitle = entity.Subtitle;
                    brand.Description = entity.Description;
                    brand.AttachmentFileReName = attachReName;
                    brand.AttachmentFileOriginalName = attachOriginalName;
                    brand.History = entity.History;
                    brand.Chat = entity.Chat;
                    brand.LinkRefer = entity.LinkRefer;
                    brand.Logo = logos;
                    _unitOfWork.BrandsRepository.Update(brand);
                    var update = await _unitOfWork.BrandsRepository.CommitAsync().ConfigureAwait(false);
                    Log.Fatal("BrandsRepository");
                    if (update)
                    {
                        Log.Fatal("UploadMultipleFile - UpLoad");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                        _uploadFileServices.UploadSingleFileAsync(logo, logos, logoOld);
                    }
                }
                else
                {
                    var model = new Brands
                    {
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        History = entity.History,
                        Chat = entity.Chat,
                        LinkRefer = entity.LinkRefer,
                        Logo = logos
                    };
                    _unitOfWork.BrandsRepository.Add(model);
                    Log.Fatal("BrandsRepository");
                    var insert = await _unitOfWork.BrandsRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        Log.Fatal("UploadMultipleFile New");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
                        _uploadFileServices.UploadSingleFileAsync(logo, logos, string.Empty);
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
                Log.Fatal("BrandsServices" + ex.Message);
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BrandsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstBrands = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspBrands_Suggestion", p);

                if (null == lstBrands)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstBrands.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BrandsServices: " + ex.Message);
                throw;
            }
        }
    }
}
