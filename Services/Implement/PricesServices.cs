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
    public class PricesServices : IPricesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public PricesServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices,
            IOptions<AppSettings> options, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
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
                var price = await _unitOfWork.PricesRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (price != null && price.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", price.Id);
                    p.Add("@Title", price.Title);
                    p.Add("@TableName", "Prices");
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
                    price.IsDeleted = true;
                    _unitOfWork.PricesRepository.Update(price);
                    await _unitOfWork.PricesRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PricesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<PriceViewModel>> GetAllAsync(PagingRequest request)
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
                var lstPrices = await _dapperRepository.QueryMultipleUsingStoreProcAsync<PriceViewModel>("uspPrices_selectAll", p);
                var priceViewModels = lstPrices.ToList();

                var total = priceViewModels.Count;
                lstPrices = !string.IsNullOrEmpty(request.SortField) ? priceViewModels.OrderBy(request) : priceViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstPrices = lstPrices.Paging(request);
                }

                if (null == lstPrices)
                {
                    return new PagedResults<PriceViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstPrices.ToList();
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
                });

                return new PagedResults<PriceViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PricesServices: " + ex.Message);
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
                var lstPrices = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspPrices_Suggestion", p);

                if (null == lstPrices)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstPrices.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " PricesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(Prices entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var price = await _unitOfWork.PricesRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (price != null && price.Id > 0)
                {
                    var fileNameOld = price.AttachmentFileReName ?? string.Empty;
                    price.History = entity.History;
                    price.Chat = entity.Chat;
                    price.Title = entity.Title;
                    price.Subtitle = entity.Subtitle;
                    price.Description = entity.Description;
                    price.AttachmentFileReName = attachReName;
                    price.AttachmentFileOriginalName = attachOriginalName;
                    price.PriceClassA = entity.PriceClassA;
                    price.PriceClassB = entity.PriceClassB;
                    price.PriceClassC = entity.PriceClassC;
                    price.PriceClassD = entity.PriceClassD;
                    price.PriceClassE = entity.PriceClassE;
                    price.PriceClassF = entity.PriceClassF;
                    price.PriceClassM = entity.PriceClassM;
                    price.PriceClassS = entity.PriceClassS;

                    _unitOfWork.PricesRepository.Update(price);
                    var update = await _unitOfWork.PricesRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                    }
                }
                else
                {
                    var model = new Prices
                    {
                        History = entity.History,
                        Chat = entity.Chat,
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        PriceClassA = entity.PriceClassA,
                        PriceClassB = entity.PriceClassB,
                        PriceClassC = entity.PriceClassC,
                        PriceClassD = entity.PriceClassD,
                        PriceClassE = entity.PriceClassE,
                        PriceClassF = entity.PriceClassF,
                        PriceClassM = entity.PriceClassM,
                        PriceClassS = entity.PriceClassS
                    };
                    _unitOfWork.PricesRepository.Add(model);
                    var insert = await _unitOfWork.PricesRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " pricesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
