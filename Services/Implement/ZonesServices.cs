using Common;
using Common.Constants;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
using ViewModel.RequestModel.Zone;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class ZonesServices : IZonesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly AppSettings _appSettings;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ZonesServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices, IOptions<AppSettings> options, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _appSettings = options.Value;
            _uploadFileServices = uploadFileServices;
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
                var zone = await _unitOfWork.ZonesRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (zone != null && zone.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", zone.Id);
                    p.Add("@Title", zone.Title);
                    p.Add("@TableName", "Zones");
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
                    zone.IsDeleted = true;
                    _unitOfWork.ZonesRepository.Update(zone);
                    await _unitOfWork.ZonesRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ZonesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<ZoneViewModel>> GetAllAsync(PagingRequest request)
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
                var lstZone = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ZoneViewModel>("uspZone_selectAll", p);
                var zoneEnumerable = lstZone.ToList();

                var total = zoneEnumerable.Count;
                lstZone = !string.IsNullOrEmpty(request.SortField) ? zoneEnumerable.OrderBy(request) : zoneEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstZone = lstZone.Paging(request);
                }

                if (null == lstZone)
                {
                    return new PagedResults<ZoneViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var zoneViewModels = lstZone.ToList();
                if (zoneViewModels.Count > 0)
                {
                    var listZone = string.Join(",", zoneViewModels.Select(x => x.Id));
                    var queryZoneColumn = $"SELECT [Id],[ZoneId],[ColumnId] FROM ZoneColumn WHERE ZoneId IN ({listZone})";
                    var zoneColumn = _dapperRepository.QueryMultiple<ZoneColumn>(queryZoneColumn);

                    zoneViewModels.ForEach(x =>
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

                        var listZoneColumn = new List<ZoneColumn>();
                        if (zoneColumn.Any())
                        {
                            listZoneColumn.AddRange(zoneColumn.Where(d => d.ZoneId == x.Id));
                        }
                        x.ListZoneColumn = listZoneColumn;
                    });
                }

                return new PagedResults<ZoneViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = zoneViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ZonesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText ?? string.Empty);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var listZone = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspZones_Suggestion", p);


                if (null == listZone)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = listZone.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ZonesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionColumnAsync(string searchText, long basementId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.BasementId, basementId);
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var listColumn = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspZones_SuggestionColumn", p);

                if (null == listColumn)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = listColumn.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ZonesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionBasementAsync(string searchText, long placeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.PlaceId, placeId);
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var listBasement = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspZones_SuggestionBasement", p);

                if (null == listBasement)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = listBasement.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ZonesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(ZoneSaveModel entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var listZoneColumn = new List<ZoneColumn>();

                if (!string.IsNullOrEmpty(entity.ListZoneColumn))
                {
                    listZoneColumn = JsonConvert.DeserializeObject<List<ZoneColumn>>(entity.ListZoneColumn);
                }

                var zone = await _unitOfWork.ZonesRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (zone != null && zone.Id > 0)
                {
                    var attachmentFileOld = zone.AttachmentFileReName ?? string.Empty;
                    zone.History = entity.History;
                    zone.Chat = entity.Chat;
                    zone.Title = entity.Title;
                    zone.Subtitle = entity.Subtitle;
                    zone.Description = entity.Description;
                    zone.AttachmentFileReName = attachReName;
                    zone.AttachmentFileOriginalName = attachOriginalName;
                    zone.PlaceId = entity.PlaceId;
                    zone.BasementId = entity.BasementId;
                    zone.ColorCodeId = entity.ColorCodeId;

                    _unitOfWork.ZonesRepository.Update(zone);
                    var update = await _unitOfWork.ZonesRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, attachmentFileOld);
                        SaveZoneColumn(zone.Id, listZoneColumn);
                    }
                }
                else
                {
                    var model = new Zones
                    {
                        History = entity.History,
                        Chat = entity.Chat,
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        PlaceId = entity.PlaceId,
                        BasementId = entity.BasementId,
                        ColorCodeId = entity.ColorCodeId
                    };
                    _unitOfWork.ZonesRepository.Add(model);
                    var insert = await _unitOfWork.ZonesRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
                        SaveZoneColumn(model.Id, listZoneColumn);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ZonesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private void SaveZoneColumn(long zoneId, List<ZoneColumn> listZoneColumn)
        {
            // delete ZoneColumn
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(Constants.Key, zoneId);
            _dapperRepository.InsertOrUpdateUsingStoreProc<ZoneColumn>("uspZoneColumn_Delete", dynamicParameters);

            if (listZoneColumn.Count <= 0) return;
            // insert ZoneColumn
            listZoneColumn.ForEach(x => x.ZoneId = zoneId);
            _unitOfWork.ZoneColumnRepository.BulkInsert(listZoneColumn);
        }
    }
}
