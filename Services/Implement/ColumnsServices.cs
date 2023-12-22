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
    public class ColumnsServices : IColumnsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ColumnsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices,
            IOptions<AppSettings> options, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _appSettings = options.Value;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IPagedResults<ColumnViewModel>> GetAllAsync(PagingRequest request)
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
                var lstColumns = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ColumnViewModel>("uspColumns_selectAll", p);
                var columnsEnumerable = lstColumns.ToList();

                var total = columnsEnumerable.Count;
                lstColumns = !string.IsNullOrEmpty(request.SortField) ? columnsEnumerable.OrderBy(request) : columnsEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstColumns = lstColumns.Paging(request);
                }

                if (null == lstColumns)
                {
                    return new PagedResults<ColumnViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var columnViewModels = lstColumns.ToList();
                columnViewModels.ForEach(x =>
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

                return new PagedResults<ColumnViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = columnViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ColumnsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(Columns entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var column = await _unitOfWork.ColumnsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (column != null && column.Id > 0)
                {
                    var fileNameOld = column.AttachmentFileReName ?? string.Empty;
                    column.History = entity.History;
                    column.Chat = entity.Chat;
                    column.Title = entity.Title;
                    column.Subtitle = entity.Subtitle;
                    column.Description = entity.Description;
                    column.AttachmentFileOriginalName = attachOriginalName;
                    column.AttachmentFileReName = attachReName;

                    _unitOfWork.ColumnsRepository.Update(column);
                    var update = await _unitOfWork.ColumnsRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                    }
                }
                else
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.IdNotFound,
                        Data = false
                    };
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ColumnsServices: " + ex.Message);
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
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstColumns = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspColumns_Suggestion", p);

                if (null == lstColumns)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstColumns.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ColumnsServices: " + ex.Message);
                throw;
            }
        }


    }
}
