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
using ViewModel.RequestModel.Teams;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class TeamsServices : ITeamsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public TeamsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices,
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
                var teams = await _unitOfWork.TeamsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (teams != null && teams.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", teams.Id);
                    p.Add("@Title", teams.Title);
                    p.Add("@TableName", "Teams");
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
                    teams.IsDeleted = true;
                    _unitOfWork.TeamsRepository.Update(teams);
                    var delete = await _unitOfWork.TeamsRepository.CommitAsync().ConfigureAwait(false);
                    if (delete)
                    {
                        var dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add(Constants.Key, id);
                        _dapperRepository.InsertOrUpdateUsingStoreProc<Teams>("uspTeams_Delete", dynamicParameters);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<TeamViewModel>> GetAllAsync(PagingRequest request)
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
                var lstWorkers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<WorkerViewModel>("uspWorkers_selectAllNoKey", null);

                var p = new DynamicParameters();
                p.Add(Constants.Key, request.SearchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstTeams = await _dapperRepository.QueryMultipleUsingStoreProcAsync<TeamViewModel>("uspTeams_selectAll", p);
                var teamViewModels = lstTeams.ToList();

                var total = teamViewModels.Count;
                lstTeams = !string.IsNullOrEmpty(request.SortField) ? teamViewModels.OrderBy(request) : teamViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstTeams = lstTeams.Paging(request);
                }

                if (null == lstTeams)
                {
                    return new PagedResults<TeamViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstTeams.ToList();
                if (viewModels.Count > 0)
                {
                    var listTeam = string.Join(",", viewModels.Select(x => x.Id));

                    var queryTeamPlaces = $"SELECT [Id],[PlaceId],[TeamId] FROM TeamPlaces WHERE TeamId IN ({listTeam})";
                    var teamPlaces = _dapperRepository.QueryMultiple<TeamPlaces>(queryTeamPlaces);
                    var listTeamPlaces = teamPlaces.ToList();

                    var queryTeamWorker = $"SELECT [Id],[WorkerId],[TeamId] FROM TeamWorker WHERE TeamId IN ({listTeam})";
                    var teamWorker = _dapperRepository.QueryMultiple<TeamWorker>(queryTeamWorker);
                    var listTeamWorker = teamWorker.ToList();

                    var queryTeamLead = $"SELECT [Id],[WorkerId],[TeamId] FROM TeamLead WHERE TeamId IN ({listTeam})";
                    var teamLead = _dapperRepository.QueryMultiple<TeamLead>(queryTeamLead);
                    var listTeamLead = teamLead.ToList();

                    var queryTeamZone = $"SELECT [Id],[ZoneId],[TeamId] FROM TeamZone WHERE TeamId IN ({listTeam})";
                    var teamZone = _dapperRepository.QueryMultiple<TeamZone>(queryTeamZone);
                    var listTeamZone = teamZone.ToList();

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

                        x.ListTeamPlaces = listTeamPlaces.FindAll(d => d.TeamId == x.Id);
                        x.ListTeamWorker = listTeamWorker.FindAll(d => d.TeamId == x.Id);
                        x.ListTeamLead = listTeamLead.FindAll(d => d.TeamId == x.Id);
                        x.ListTeamZone = listTeamZone.FindAll(d => d.TeamId == x.Id);
                        var listPlaceId = x.ListTeamPlaces.Select(z => z.PlaceId).ToArray();
                        var listPlaces = _unitOfWork.PlacesRepository.GetAll().Where(z => listPlaceId.Any(y => y == z.Id)).Select(z => z.Title).ToArray();
                        x.Places = string.Join(", ", listPlaces);

                        var listWorkerId = x.ListTeamWorker.Select(z => z.WorkerId).ToList();
                        var listworkers = lstWorkers.Where(z => listWorkerId.Any(y => y == z.Id)).Select(z => z.FullName).ToArray();
                        x.Workers = string.Join(", ", listworkers);

                        var listLeadId = x.ListTeamLead.Select(z => z.WorkerId).ToArray();
                        var listLeads = lstWorkers.Where(z => listLeadId.Any(y => y == z.Id)).Select(z => z.FullName).ToArray();
                        x.Leads = string.Join(", ", listLeads);

                        var listZoneId = x.ListTeamZone.Select(z => z.ZoneId).ToArray();
                        var listZones = _unitOfWork.ZonesRepository.GetAll().Where(z => listZoneId.Any(y => y == z.Id)).Select(z => z.Title).ToArray();
                        x.Zones = string.Join(", ", listZones);
                    });
                }

                return new PagedResults<TeamViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsServices: " + ex.Message);
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
                var lstTeams = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspTeams_Suggestion", p);

                if (null == lstTeams)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstTeams.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(TeamSaveModel entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var listTeamPlaces = new List<TeamPlaces>();
                var listTeamWorker = new List<TeamWorker>();
                var listTeamLead = new List<TeamLead>();
                var listTeamZone = new List<TeamZone>();

                if (!string.IsNullOrEmpty(entity.ListTeamPlaces))
                {
                    listTeamPlaces = JsonConvert.DeserializeObject<List<TeamPlaces>>(entity.ListTeamPlaces);
                }

                if (!string.IsNullOrEmpty(entity.ListTeamWorker))
                {
                    listTeamWorker = JsonConvert.DeserializeObject<List<TeamWorker>>(entity.ListTeamWorker);
                }

                if (!string.IsNullOrEmpty(entity.ListTeamLead))
                {
                    listTeamLead = JsonConvert.DeserializeObject<List<TeamLead>>(entity.ListTeamLead);
                }

                if (!string.IsNullOrEmpty(entity.ListTeamZone))
                {
                    listTeamZone = JsonConvert.DeserializeObject<List<TeamZone>>(entity.ListTeamZone);
                }

                var teams = await _unitOfWork.TeamsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (teams != null && teams.Id > 0)
                {
                    var attachmentFileOld = teams.AttachmentFileReName ?? string.Empty;
                    teams.History = entity.History;
                    teams.Chat = entity.Chat;
                    teams.Title = entity.Title;
                    teams.Subtitle = entity.Subtitle;
                    teams.Description = entity.Description;
                    teams.AttachmentFileReName = attachReName;
                    teams.AttachmentFileOriginalName = attachOriginalName;
                    teams.ColorCodeId = entity.ColorCodeId;

                    _unitOfWork.TeamsRepository.Update(teams);
                    var update = await _unitOfWork.TeamsRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, attachmentFileOld);

                        // delete Team
                        var paramUpdate = new DynamicParameters();
                        paramUpdate.Add(Constants.Key, teams.Id);
                        _dapperRepository.InsertOrUpdateUsingStoreProc<Teams>("uspTeams_Delete", paramUpdate);

                        SaveTeamPlaces(teams.Id, listTeamPlaces);
                        SaveTeamLead(teams.Id, listTeamLead);
                        SaveTeamWorker(teams.Id, listTeamWorker);
                        SaveTeamZone(teams.Id, listTeamZone);
                    }
                }
                else
                {
                    var model = new Teams
                    {
                        History = entity.History,
                        Chat = entity.Chat,
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        ColorCodeId = entity.ColorCodeId
                    };
                    _unitOfWork.TeamsRepository.Add(model);
                    var insert = await _unitOfWork.TeamsRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);

                        // delete Team
                        var paramInsert = new DynamicParameters();
                        paramInsert.Add(Constants.Key, model.Id);
                        _dapperRepository.InsertOrUpdateUsingStoreProc<Teams>("uspTeams_Delete", paramInsert);

                        SaveTeamPlaces(model.Id, listTeamPlaces);
                        SaveTeamLead(model.Id, listTeamLead);
                        SaveTeamWorker(model.Id, listTeamWorker);
                        SaveTeamZone(model.Id, listTeamZone);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private void SaveTeamPlaces(long teamId, List<TeamPlaces> listTeamPlaces)
        {
            if (listTeamPlaces.Count <= 0) return;
            // insert TeamPlaces
            listTeamPlaces.ForEach(x => x.TeamId = teamId);
            _unitOfWork.TeamPlacesRepository.BulkInsert(listTeamPlaces);
        }

        private void SaveTeamLead(long teamId, List<TeamLead> listTeamLead)
        {
            if (listTeamLead.Count <= 0) return;
            // insert TeamLead
            listTeamLead.ForEach(x => x.TeamId = teamId);
            _unitOfWork.TeamLeadRepository.BulkInsert(listTeamLead);
        }

        private void SaveTeamWorker(long teamId, List<TeamWorker> listTeamWorker)
        {
            if (listTeamWorker.Count <= 0) return;
            // insert TeamWorker
            listTeamWorker.ForEach(x => x.TeamId = teamId);
            _unitOfWork.TeamWorkerRepository.BulkInsert(listTeamWorker);
        }

        private void SaveTeamZone(long teamId, List<TeamZone> listTeamZones)
        {
            if (listTeamZones.Count <= 0) return;
            // insert TeamZone
            listTeamZones.ForEach(x => x.TeamId = teamId);
            _unitOfWork.TeamZoneRepository.BulkInsert(listTeamZones);
        }

        public IPagedResults<ItemModel> SuggestionZoneByPlaceAsync(string searchText, string listTeamPlace)
        {
            try
            {
                var lstTeamPlace = new List<TeamPlaces>();

                if (!string.IsNullOrEmpty(listTeamPlace))
                {
                    lstTeamPlace = JsonConvert.DeserializeObject<List<TeamPlaces>>(listTeamPlace);
                }

                if (lstTeamPlace.Count <= 0)
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                        ListData = new List<ItemModel>(),
                        TotalRecords = 0
                    };
                var listResult = string.Join(", ", lstTeamPlace.Select(x => x.PlaceId));
                var query = @"
                            DECLARE @company NVARCHAR(100)
		                    select TOP 1 @company = CompanyShareds from B2B 
		                    where CompanyId={CompanyId} and DataType='Workers';

                            SELECT DISTINCT Id, Title AS [Value] FROM dbo.Zones WHERE PlaceId IN ({listResult}) AND ( ISNULL('{searchText}', '') = '' OR dbo.ufnRemoveMark(TRIM(UPPER(Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER('{searchText}'))),'%') ) AND IsDeleted = 0 And (CompanyId = {CompanyId} OR CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ',')))";
                query = query.Replace("{listResult}", listResult);
                query = query.Replace("{searchText}", searchText);
                query = query.Replace("{CompanyId}", _authenticatedUser.CompanyId.ToString());
                var listZone = _dapperRepository.QueryMultiple<ItemModel>(query);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsServices: " + ex.Message);
                throw;
            }
        }

        public IPagedResults<ItemModel> SuggestionWorkerByPlaceAsync(string searchText, string listTeamPlace)
        {
            try
            {
                var lstTeamPlace = new List<TeamPlaces>();

                if (!string.IsNullOrEmpty(listTeamPlace))
                {
                    lstTeamPlace = JsonConvert.DeserializeObject<List<TeamPlaces>>(listTeamPlace);
                }

                if (lstTeamPlace.Count <= 0)
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                        ListData = new List<ItemModel>(),
                        TotalRecords = 0
                    };

                var listResult = string.Join(", ", lstTeamPlace.Select(x => x.PlaceId));
                var query = @"
                            DECLARE @company NVARCHAR(100)
		                    select TOP 1 @company = CompanyShareds from B2B 
		                    where CompanyId={CompanyId} and DataType='Workers';

                            SELECT DISTINCT w.Id, up.FullName AS [Value] FROM dbo.Workers AS w INNER JOIN dbo.WorkerPlace AS wp ON wp.WorkerId = w.Id AND wp.PlaceId IN ({listResult}) INNER JOIN dbo.UserProfile AS up ON up.Id = w.UserId AND up.Active = 1 INNER JOIN dbo.UserRoles AS ur ON ur.UserId = up.Id INNER JOIN dbo.Roles AS r ON r.Id = ur.RoleId AND r.[Name] = 'Worker' WHERE w.IsDeleted = 0 And (w.CompanyId = {CompanyId} OR w.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))) AND ( ISNULL('{searchText}', '') = '' OR dbo.ufnRemoveMark(TRIM(UPPER(up.FullName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER('{searchText}'))),'%') ) ";
                query = query.Replace("{listResult}", listResult);
                query = query.Replace("{searchText}", searchText);
                query = query.Replace("{CompanyId}", _authenticatedUser.CompanyId.ToString());
                var listBasement = _dapperRepository.QueryMultiple<ItemModel>(query);

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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsServices: " + ex.Message);
                throw;
            }
        }
    }
}
