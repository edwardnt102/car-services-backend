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
    public class B2BServices : IB2BServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public B2BServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices, IOptions<AppSettings> options, IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _appSettings = options.Value;
            _authenticatedUserService = authenticatedUserService;
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
                var b2b = await _unitOfWork.B2BRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (b2b != null && b2b.Id > 0)
                {
                    b2b.IsDeleted = true;
                    _unitOfWork.B2BRepository.Update(b2b);
                    await _unitOfWork.B2BRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " B2BServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<B2BViewModel>> GetAllAsync(PagingRequest request)
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
                var lstB2B = await _dapperRepository.QueryMultipleUsingStoreProcAsync<B2BViewModel>("uspB2B_selectAll", p);
                var b2BEnumerable = lstB2B.ToList();

                var total = b2BEnumerable.Count;
                lstB2B = !string.IsNullOrEmpty(request.SortField) ? b2BEnumerable.OrderBy(request) : b2BEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstB2B = lstB2B.Paging(request);
                }

                if (null == lstB2B)
                {
                    return new PagedResults<B2BViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var b2BViewModels = lstB2B.ToList();
                if (b2BViewModels.Count > 0)
                {
                    b2BViewModels.ForEach(x =>
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

                        var share = string.Empty;
                        if (!string.IsNullOrEmpty(x.CompanyShareds))
                        {
                            var query = $"SELECT [Title] FROM Company WHERE [Id] IN ({x.CompanyShareds}) AND IsDeleted = 0";
                            var data = _dapperRepository.QueryMultiple<Company>(query);
                            share = string.Join(",", data.Select(x => x.Title));
                        }
                        x.CompanySharedName = share;
                    });
                }

                return new PagedResults<B2BViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = b2BViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " B2BServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(B2B entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var b2B = await _unitOfWork.B2BRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (b2B != null && b2B.Id > 0)
                {
                    var fileNameOld = b2B.AttachmentFileReName ?? string.Empty;
                    b2B.Title = entity.Title;
                    b2B.Subtitle = entity.Subtitle;
                    b2B.Description = entity.Description;
                    b2B.AttachmentFileReName = attachReName;
                    b2B.AttachmentFileOriginalName = attachOriginalName;
                    b2B.History = entity.History;
                    b2B.Chat = entity.Chat;
                    b2B.CompanyShareds = entity.CompanyShareds;
                    b2B.DataType = entity.DataType;
                    b2B.CompanyId = entity.CompanyId;
                    b2B.CreatedDate = DateTime.Now;
                    b2B.CreatedBy = _authenticatedUserService.UserId;
                    b2B.IsDeleted = false;
                    _unitOfWork.B2BRepository.Update(b2B);
                    var update = _unitOfWork.B2BRepository.Commit();
                    Log.Fatal("B2BRepository");
                    if (update)
                    {
                        Log.Fatal("UploadMultipleFile - UpLoad");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                    }
                }
                else
                {
                    var model = new B2B
                    {
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        History = entity.History,
                        Chat = entity.Chat,
                        CompanyShareds = entity.CompanyShareds,
                        DataType = entity.DataType,
                        CompanyId = entity.CompanyId,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = _authenticatedUserService.UserId
                    };
                    _unitOfWork.B2BRepository.Add(model);
                    Log.Fatal("B2BRepository");
                    var insert = _unitOfWork.B2BRepository.Commit();
                    if (insert)
                    {
                        Log.Fatal("UploadMultipleFile New");
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
                Log.Fatal("B2BServices" + ex.Message);
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " B2BServices: " + ex.Message);
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
                var lstB2B = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspB2B_Suggestion", p);

                if (null == lstB2B)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstB2B.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " B2BServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionDataTypeAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var listData = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspDataType_Suggestion", p);

                if (null == listData)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = listData.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " B2BServices: " + ex.Message);
                throw;
            }
        }
    }
}
