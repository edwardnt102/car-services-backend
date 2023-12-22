using ClosedXML.Excel;
using Common;
using Common.Constants;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Hosting;
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
using Utility;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel.Columns;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class BasementsServices : IBasementsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private static string _webRootPath = string.Empty;
        private readonly AppSettings _appSettings;

        public BasementsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices, IHostingEnvironment hostingEnvironment,
            IAuthenticatedUserService authenticatedUserService, IOptions<AppSettings> options)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _webRootPath = hostingEnvironment.WebRootPath + Constants.Upload;
            _authenticatedUserService = authenticatedUserService;
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
                var basement = await _unitOfWork.BasementsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (basement != null && basement.Id > 0)
                {

                    var p = new DynamicParameters();
                    p.Add("@Id", basement.Id);
                    p.Add("@Title", basement.Title);
                    p.Add("@TableName", "Basements");
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

                    basement.IsDeleted = true;
                    _unitOfWork.BasementsRepository.Update(basement);
                    var result = await _unitOfWork.BasementsRepository.CommitAsync().ConfigureAwait(false);
                    if (result)
                    {
                        var dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add(Constants.Key, id);
                        _dapperRepository.InsertOrUpdateUsingStoreProc<Columns>("uspColumns_Delete", dynamicParameters);
                    }
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BasementsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<BasementViewModel>> GetAllAsync(PagingRequest request)
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
                p.Add(Constants.CompanyId, _authenticatedUserService.CompanyId);
                var lstBasements = await _dapperRepository.QueryMultipleUsingStoreProcAsync<BasementViewModel>("uspBasements_selectAll", p);
                var basementsEnumerable = lstBasements.ToList();

                var total = basementsEnumerable.Count;
                lstBasements = !string.IsNullOrEmpty(request.SortField) ? basementsEnumerable.OrderBy(request) : basementsEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstBasements = lstBasements.Paging(request);
                }

                if (null == lstBasements)
                {
                    return new PagedResults<BasementViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstBasements.ToList();
                if (viewModels.Count > 0)
                {
                    var listBasement = string.Join(",", viewModels.Select(x => x.Id));

                    var queryColumn = $"SELECT [Title], BasementId FROM Columns WHERE [BasementId] IN ({listBasement})";
                    var column = _dapperRepository.QueryMultiple<Columns>(queryColumn);
                    var listColumn = column.ToList();

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

                        x.DiagramAttachmentReName = !string.IsNullOrEmpty(x.DiagramAttachmentReName) ? _appSettings.DomainFile + x.DiagramAttachmentReName : string.Empty;
                        var listColumns = listColumn.Where(d => d.BasementId == x.Id).Select(y => y.Title).ToList();
                        x.ListColumn = listColumns.Count > 0 ? string.Join(", ", listColumns) : string.Empty;
                    });
                }

                return new PagedResults<BasementViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BasementsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText, long? placeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add("@PlaceId", placeId ?? 0);
                p.Add(Constants.CompanyId, _authenticatedUserService.CompanyId);
                var lstBasements = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspBasements_Suggestion", p);

                if (null == lstBasements)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstBasements.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BasementsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(Basements entity, IFormFile file, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var basement = await _unitOfWork.BasementsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachmentFileNew = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachmentFileOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                var diagramAttachmentNew = _uploadFileServices.GetFileNameSingleFileReNameAsync(file);
                var diagramAttachmentOriginalName = _uploadFileServices.GetFileNameSingleFileOriginalNameAsync(file);
                if (basement != null && basement.Id > 0)
                {
                    var attachmentFileOld = basement.AttachmentFileReName ?? string.Empty;
                    basement.History = entity.History;
                    basement.Chat = entity.Chat;
                    basement.Title = entity.Title;
                    basement.Subtitle = entity.Subtitle;
                    basement.Description = entity.Description;
                    basement.AttachmentFileReName = attachmentFileNew;
                    basement.AttachmentFileOriginalName = attachmentFileOriginalName;
                    basement.PlaceId = entity.PlaceId;

                    _unitOfWork.BasementsRepository.Update(basement);
                    var update = await _unitOfWork.BasementsRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachmentFileNew, attachmentFileOld);
                    }
                }
                else
                {
                    var model = new Basements
                    {
                        History = entity.History,
                        Chat = entity.Chat,
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachmentFileNew,
                        AttachmentFileOriginalName = attachmentFileOriginalName,
                        PlaceId = entity.PlaceId,
                        DiagramAttachmentReName = diagramAttachmentNew,
                        DiagramAttachmentOriginalName = diagramAttachmentOriginalName
                    };
                    _unitOfWork.BasementsRepository.Add(model);
                    var insert = await _unitOfWork.BasementsRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachmentFileNew, string.Empty);
                        var upload = _uploadFileServices.UploadSingleFileAsync(file, diagramAttachmentNew, string.Empty);
                        if (upload)
                        {
                            Log.Fatal($"====={diagramAttachmentNew}===");
                            var getDataFromExcel = GetDataFromExcel(diagramAttachmentNew);
                            if (getDataFromExcel.Count > 0)
                            {
                                var listColumns = new List<ColumnsModel>();
                                var userId = _authenticatedUserService.UserId;
                                listColumns.AddRange(getDataFromExcel.Select(item => new ColumnsModel
                                {
                                    Title = item,
                                    BasementId = model.Id,
                                    CreatedBy = userId,
                                    ModifiedBy = userId,
                                    IsDeleted = false
                                }));
                                var dynamicParameters = new DynamicParameters();
                                dynamicParameters.Add(Constants.Key, CreateDataTable(listColumns));
                                await _unitOfWork.ColumnsRepository.BulkInsertUsingDapper("uspColumn_BulkInsert", dynamicParameters);
                            }
                        }
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BasementsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private List<string> GetDataFromExcel(string diagramAttachmentNew)
        {
            var listColumn = new List<string>();
            using var workbook = new XLWorkbook(_webRootPath + Constants.Source + diagramAttachmentNew);
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.RangeUsed();
            if (range == null) return listColumn;
            for (var i = 1; i <= range.RowCount() + 2; i++)
            {
                for (var j = 1; j <= range.ColumnCount() + 2; j++)
                {
                    if (!string.IsNullOrEmpty(worksheet.Cell(i, j).Value.ToString()) && !string.IsNullOrWhiteSpace(worksheet.Cell(i, j).Value.ToString()))
                    {
                        listColumn.Add(worksheet.Cell(i, j).Value.ToString());
                    }
                }
            }
            return listColumn;
        }

        private object CreateDataTable(List<ColumnsModel> listColumns)
        {
            var data = DataTableUtility.ListObjectToDataTable(listColumns, "ColumnsModel");
            return data.AsTableValuedParameter();
        }


    }
}
