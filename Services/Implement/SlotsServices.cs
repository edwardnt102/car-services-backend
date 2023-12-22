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
using System.Text.Json;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel;
using ViewModel.RequestModel.Slots;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class SlotsServices : ISlotsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly AppSettings _appSettings;

        public SlotsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices,
            IAuthenticatedUserService authenticatedUser, IOptions<AppSettings> options)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _authenticatedUser = authenticatedUser;
            _appSettings = options.Value;
        }

        public async Task<IPagedResult<bool>> SaveAsync(StaffUpdate entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var slot = GetSlots(entity.Id);
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }
                var fileNameOld = slot.AttachmentFileReName ?? string.Empty;

                slot.Title = entity.Title;
                slot.Subtitle = entity.Subtitle;
                slot.Description = entity.Description;
                slot.AttachmentFileReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                //slot.RuleId = entity.RuleId;
                slot.UnexpectedBonus = entity.UnexpectedBonus;
                slot.UnexpectedPenalty = entity.UnexpectedPenalty;

                _unitOfWork.SlotsRepository.Update(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _uploadFileServices.UploadMultipleFileAsync(files, slot.AttachmentFileReName, fileNameOld);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveFail,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
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
                var slot = await _unitOfWork.SlotsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (slot != null && slot.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", slot.Id);
                    p.Add("@Title", slot.Title);
                    p.Add("@TableName", "Slots");
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
                    slot.IsDeleted = true;
                    _unitOfWork.SlotsRepository.Update(slot);
                    await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.DeleteSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " BrandsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public IPagedResults<SlotViewModel> GetAllAsync(RequestGetSlot request)
        {
            try
            {
                request = !request.Page.HasValue ? new RequestGetSlot
                {
                    Page = 1,
                    PageSize = 10,
                    SortDir = Constants.SortDesc,
                    SortField = Constants.SortId
                } : request;
                var p = new DynamicParameters();

                p.Add(Constants.TeamName, request.TeamName);
                p.Add(Constants.WorkerName, request.WorkerName);
                p.Add(Constants.PlaceName, request.PlaceName);
                p.Add(Constants.Day, request.Day.HasValue ? request.Day.Value : DateTime.Now.Date);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var query = @"
                            DECLARE @company NVARCHAR(100)
		                      select TOP 1 @company = CompanyShareds from B2B 
		                      where CompanyId=@CompanyId and DataType='Slots';

                            select s.Id, s.Day as Day, t.Title as TeamName, u.FullName as WorkerName, p.Title as PlaceName, r.Title as RuleName, s.BookStatus as BookStatus, s.*, c.Title as CompanyName
                            From dbo.Slots s 
                            left join dbo.Places p on s.PlaceId = p.Id AND p.IsDeleted=0
                            left join dbo.TeamWorker tw on s.WorkerId = tw.WorkerId
                            left join dbo.Workers w on tw.WorkerId = w.Id AND w.IsDeleted=0
							       left join dbo.UserProfile u on w.UserId = u.Id AND u.IsDeleted=0
                            left join dbo.Teams t on tw.TeamId = t.Id AND t.IsDeleted=0
                            left join dbo.Rules r on s.RuleId = r.Id AND r.IsDeleted=0
                            left join dbo.Company c on c.Id = s.CompanyId AND c.IsDeleted=0";
                if (!string.IsNullOrEmpty(request.TeamName)
                    || !string.IsNullOrEmpty(request.PlaceName)
                    || !string.IsNullOrEmpty(request.WorkerName)
                    || request.Day != null)
                {
                    var first = true;
                    query += "Where s.IsDeleted = 0 And (s.CompanyId = @CompanyId OR s.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))) AND ";
                    if (!string.IsNullOrEmpty(request.TeamName))
                    {
                        if (first)
                        {
                            query += "t.Title = @TeamName ";
                            first = !first;
                        }
                        else
                        {
                            query += "and t.Title = @TeamName ";
                        }
                    }
                    if (!string.IsNullOrEmpty(request.WorkerName))
                    {
                        if (first)
                        {
                            query += "w.Title = @WorkerName ";
                            first = !first;
                        }
                        else
                        {
                            query += "and w.Title = @WorkerName ";
                        }
                    }
                    if (!string.IsNullOrEmpty(request.PlaceName))
                    {
                        if (first)
                        {
                            query += "p.Title = @PlaceName ";
                            first = !first;
                        }
                        else
                        {
                            query += "and p.Title = @PlaceName ";
                        }
                    }

                }
                var lstSlots = _dapperRepository.QueryMultipleWithParam<SlotViewModel>(query, p).Skip((request.Page.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value);
                var slotEnumerable = lstSlots.ToList();

                var total = slotEnumerable.Count;
                lstSlots = !string.IsNullOrEmpty(request.SortField) ? slotEnumerable.OrderBy(request) : slotEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstSlots = lstSlots.Paging(request);
                }

                if (null == lstSlots)
                {
                    return new PagedResults<SlotViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var slotViewModels = lstSlots.ToList();
                slotViewModels.ForEach(x =>
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
                    x.CheckInImage = !string.IsNullOrEmpty(x.CheckInImage) ? _appSettings.DomainFile + x.CheckInImage : x.CheckInImage;
                    x.CheckOutImage = !string.IsNullOrEmpty(x.CheckOutImage) ? _appSettings.DomainFile + x.CheckOutImage : x.CheckOutImage;
                });

                return new PagedResults<SlotViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = slotViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
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
                var lstSlots = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspSlots_Suggestion", p);


                if (null == lstSlots)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstSlots.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                throw;
            }
        }


        public IPagedResults<ItemModel> GetTeamsAsync(long? workerId)
        {
            var workId = workerId != null ? workerId : _unitOfWork.WorkersRepository.FindBy(x => x.UserId.ToLower().Equals(_authenticatedUser.UserId.ToLower()) && x.IsDeleted.Equals(false)).FirstOrDefault()?.Id;

            if (workId == null)
            {
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.WorkerNotFound,
                    ListData = null
                };
            }
            var teamsWorker = _unitOfWork.TeamWorkerRepository.FindBy(x => x.WorkerId == workId)?.Select(x => x.TeamId);
            if (teamsWorker == null)
            {
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.TeamNotFound,
                    ListData = null
                };
            }
            var teams = _unitOfWork.TeamsRepository.FindBy(x => teamsWorker.Contains(x.Id))?.Distinct().Select(x => new ItemModel
            {
                Id = x.Id,
                Value = x.Title
            }).ToList();
            return new PagedResults<ItemModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                ListData = teams,
                TotalRecords = teams.Count
            };
        }

        public IPagedResults<ItemModel> GetPlacesAsync(long? teamId)
        {
            var p = new DynamicParameters();
            p.Add("@UserId", _authenticatedUser.UserId);
            var query = @"select distinct p.id as Id, p.Title as Value
                        from dbo.workers w
                        join dbo.TeamWorker tw on w.id = tw.workerId
                        join dbo.TeamPlaces tp on tp.TeamId = tw.TeamId
                        join dbo.places p on tp.placeId = p.id
                        where w.UserId = @UserId ";
            if (teamId == null)
            {
                var response = _dapperRepository.QueryMultipleWithParam<ItemModel>(query, p).ToList();
                if (response == null)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.PlaceNotFound,
                        ListData = null
                    };
                }
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = response,
                    TotalRecords = response.Count
                };
            }
            p.Add("@TeamId", teamId);
            query += "and tp.teamId = @TeamId";
            var place = _dapperRepository.QueryMultipleWithParam<ItemModel>(query, p).ToList();
            var teamPlaces = _unitOfWork.TeamPlacesRepository.FindBy(x => x.TeamId == teamId)?.Select(x => x.PlaceId);
            if (teamPlaces == null)
            {
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.TeamNotFound,
                    ListData = null
                };
            }
            var places = _unitOfWork.PlacesRepository.FindBy(x => teamPlaces.Contains(x.Id))?
                .Select(x => new ItemModel
                {
                    Id = x.Id,
                    Value = x.Title
                }).ToList();
            if (place == null)
            {
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.PlaceNotFound,
                    ListData = null
                };
            }
            return new PagedResults<ItemModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                ListData = place,
                TotalRecords = place.Count
            };
        }



        public async Task<IPagedResult<bool>> CreateSlotAsync(CreateSlot createSlot)
        {
            try
            {
                await _unitOfWork.OpenTransaction();

                var p = new DynamicParameters();
                var userId = _unitOfWork.WorkersRepository.GetSingle(x => x.Id == createSlot.WorkerId)?.UserId;
                p.Add("@UserId", userId ?? _authenticatedUser.UserId);
                p.Add("@TeamId", createSlot.TeamId);
                p.Add("@PlaceId", createSlot.PlaceId);

                var query = @"Select w.id as WorkerId, p.Id as PlaceId, tp.TeamId as TeamId, r.id as RuleId, 
                            r.PileRegistration as PileRegistration, w.WorkerType
                            from dbo.Workers w join dbo.TeamWorker tw on w.id = tw.workerId
                            join dbo.TeamPlaces tp on tw.teamId = tp.TeamId
                            join dbo.Places p on tp.PlaceId = p.Id
                            join dbo.Rules r on p.ruleId = r.Id
                            where tp.TeamId = @TeamId
                            and p.id = @PlaceId
                            and w.UserId = @UserId";
                var response = _dapperRepository.QueryMultipleWithParam<CreateSlotResponse>(query, p).FirstOrDefault();
                if (response == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.CreateSlotFail,
                        Data = false
                    };
                }
                var slotExist = _unitOfWork.SlotsRepository.FindBy(x => (x.WorkerId == response.WorkerId && x.Day.Value.Date == createSlot.Day.Value.Date));
                if (slotExist != null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.CreateSlotExist,
                        Data = false
                    };
                }

                var slot = new Slots()
                {
                    Title = createSlot.Title,
                    Day = createSlot.Day,
                    WorkerId = response.WorkerId,
                    PlaceId = response.PlaceId,
                    TeamId = response.TeamId,
                    RuleId = response.RuleId,
                    PileRegistration = response.PileRegistration,
                    BookStatus = response.WorkerType == (int)WorkerType.EXPERT ? StatusEnum.Approved.GetDescription() : StatusEnum.Wait.GetDescription(),
                    NumberOfRegisteredVehicles = createSlot.NumberOfRegisteredVehicles
                };


                _unitOfWork.SlotsRepository.Add(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.CreateSlotSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.CreateSlotFail,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<bool>> CancelSlotAsync(CancelSlot cancelSlot)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var slot = GetSlots(cancelSlot.Id);
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }

                slot.BookStatus = StatusEnum.Cancel.GetDescription();
                slot.ReasonCancel = cancelSlot.ReasonCancel;

                //Phat huy lich
                var rule = _unitOfWork.RulesRepository.GetSingle(x => x.Id == slot.RuleId);
                decimal penalty = 0;
                if (rule.CancellationOfSchedulePenalty != null)
                {
                    var penaltyJson = JsonSerializer.Deserialize<CancellationOfSchedulePenalty>(rule.CancellationOfSchedulePenalty);
                    if (slot.BookStatus == StatusEnum.Cancel.ToString() && String.IsNullOrEmpty(penaltyJson.NoReplacePenalty))
                    {
                        var noReplacePenalty = JsonSerializer.Deserialize<List<RuleRewardAndPunish>>(penaltyJson.NoReplacePenalty);
                        if (noReplacePenalty != null && noReplacePenalty.Any())
                        {
                            int i = 0;
                            var latestModified = slot.ModifiedDate ?? slot.CreatedDate;
                            while (DateTime.Now.AddDays(noReplacePenalty[i].Unit) <= slot.Day)
                            {
                                i++;
                            }
                            penalty = i < (noReplacePenalty.Count) ? (decimal)noReplacePenalty[i].Price : (decimal)0;
                        }
                    }
                }

                slot.UnexpectedPenalty = penalty;
                slot.TotalAmount = 0 - penalty - slot.PileRegistration;
                _unitOfWork.SlotsRepository.Update(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.CancelSlotSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.CancelSlotFail,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<bool>> CheckInAsync(IFormFile file)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
                if (worker == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        Data = false
                    };
                }

                var slot = _unitOfWork.SlotsRepository.GetSingle(x=>x.WorkerId == worker.Id && x.Day.Value.Date == DateTime.Now.Date);
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }

                if (!slot.BookStatus.ToLower().Equals(StatusEnum.Approved.GetDescription().ToLower()))
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.SlotNotApproved,
                        Data = false
                    };
                }

                var oldCheckInImage = slot.CheckInImage ?? string.Empty;
                slot.CheckInTime = DateTime.Now;
                slot.BookStatus = StatusEnum.Working.ToString();
                slot.CheckInImage = _uploadFileServices.GetFileNameSingleFileReNameAsync(file);


                _unitOfWork.SlotsRepository.Update(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _uploadFileServices.UploadSingleFileAsync(file, slot.CheckInImage, oldCheckInImage);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.CheckInSlotSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.CheckInSlotFail,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<bool>> CheckOutAsync(CheckOut checkOut, IFormFile file)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
                if (worker == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        Data = false
                    };
                }

                var slot = _unitOfWork.SlotsRepository.GetSingle(x => x.WorkerId == worker.Id && x.Day.Value.Date == DateTime.Now.Date);
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }
                if (slot.BookStatus == StatusEnum.Working.ToString())
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.CheckOutSlotFail,
                        Data = false
                    };
                }
                var rule = await _unitOfWork.RulesRepository.GetSingleAsync(x => (x.PlaceId != null
                && x.PlaceId == slot.PlaceId) && (x.Day.HasValue && x.Day.Value.Date == DateTime.Now.Date));
                if (rule != null)
                {
                    slot.RuleId = rule.Id;
                    slot.IncomeDeadline = DateTime.Now.AddDays(rule.DayPayroll);
                    slot.PileRegistration = rule.PileRegistration;
                }
                else
                {
                    var ruleDefault = await _unitOfWork.RulesRepository.GetSingleAsync(x => x.Id == slot.RuleId);
                    slot.RuleId = ruleDefault.Id;
                    slot.IncomeDeadline = DateTime.Now.AddDays(ruleDefault.DayPayroll);
                    slot.PileRegistration = ruleDefault.PileRegistration;
                }

                var oldCheckOutImage = slot.CheckOutImage ?? string.Empty;
                slot.CheckOutTime = DateTime.Now;
                slot.BookStatus = StatusEnum.WaitForPay.ToString();
                slot.CheckOutImage = _uploadFileServices.GetFileNameSingleFileReNameAsync(file);
                slot.SuppliesReturned = checkOut.SuppliesReturned;
                slot.ChemicalReturns = checkOut.ChemicalReturns;

                _unitOfWork.SlotsRepository.Update(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _uploadFileServices.UploadSingleFileAsync(file, slot.CheckOutImage, oldCheckOutImage);

                    var salary = (await GetSlotSalaryAsync(slot.Id)).Data;
                    if (salary != null)
                    {
                        slot.TotalAmount = salary.TotalAmount;
                    }
                    _unitOfWork.SlotsRepository.Update(slot);
                    await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.CheckOutSlotSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.CheckOutSlotFail,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Error(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private Slots GetSlots(long id)
        {
            return _unitOfWork.SlotsRepository.GetSingle(x => x.Id == id);
        }

        public async Task<IPagedResult<bool>> ApproveSlotAsync(long id)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var slot = GetSlots(id);
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }

                if (!slot.BookStatus.ToLower().Equals(StatusEnum.Wait.GetDescription().ToLower()))
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.ApproveSlotFail,
                        Data = false
                    };
                }


                slot.BookStatus = StatusEnum.Approved.GetDescription();
                _unitOfWork.SlotsRepository.Update(slot);

                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.ApproveSlotSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.ApproveSlotFail,
                    Data = false
                };
            }

            catch (Exception ex)
            {
                Log.Error(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<bool>> UpdateSlotAsync(UpdateSlot updateSlot)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var slot = GetSlots(updateSlot.Id);
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }

                slot.Day = updateSlot.Day ?? slot.Day;
                slot.NumberOfVehiclesReRegistered = updateSlot.NumberOfVehiclesReRegistered ?? slot.NumberOfRegisteredVehicles;
                slot.TeamId = updateSlot.TeamId ?? slot.TeamId;
                slot.PlaceId = updateSlot.PlaceId ?? slot.PlaceId;
                slot.SuppliedMaterials = updateSlot.SupplyMaterials ?? slot.SuppliedMaterials;
                slot.SuppliesReturned = updateSlot.SupplyReturned ?? slot.SuppliesReturned;
                slot.ChemicalLevel = updateSlot.ChemicalLevel ?? slot.ChemicalLevel;
                slot.ChemicalReturns = updateSlot.ChemicalReturn ?? slot.ChemicalReturns;
                slot.RuleId = updateSlot.RuleId ?? slot.RuleId;
                slot.UnexpectedBonus = updateSlot.UnexpectedBonus ?? 0;
                slot.UnexpectedPenalty = updateSlot.UnexpectedPenalty ?? 0;

                _unitOfWork.SlotsRepository.Update(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.UpdateSlotSuccess,
                        Data = true
                    };
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.UpdateSlotFail,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<BonusSalary>> GetSlotSalaryAsync(long slotId)
        {
            try
            {
                var salary = new BonusSalary();

                var slot = _unitOfWork.SlotsRepository.GetSingle(x => x.Id == slotId);
                if (slot == null)
                {
                    return new PagedResult<BonusSalary>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = salary
                    };
                }

                var rule = _unitOfWork.RulesRepository.GetSingle(x => x.Id == slot.RuleId);
                if (rule == null)
                {
                    return new PagedResult<BonusSalary>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = salary
                    };
                }
                // Tien coc
                var pileRegistration = rule.PileRegistration ?? 10000;


                // Kiem tra dieu kien rule
                if (slot.NumberOfVehiclesReRegistered < rule.MinimumQuantity)
                {
                    return new PagedResult<BonusSalary>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.RulesNotAvailableMessage,
                        Data = salary
                    };
                }
                float? signUpBonus = 0;
                // Thuong dang ky
                if (slot.NumberOfVehiclesReRegistered > rule.MinimumQuantity)
                {
                    var signUpBonusJson = JsonSerializer.Deserialize<List<RuleRewardAndPunish>>(rule.SignUpBonus);
                    if (signUpBonusJson != null && signUpBonusJson.Any())
                    {
                        var day = signUpBonusJson[0].Unit;
                        var latestModified = slot.ModifiedDate ?? slot.CreatedDate;
                        var i = 0;
                        if (latestModified.HasValue)
                        {
                            while (latestModified.Value.AddDays(signUpBonusJson[i].Unit) < slot.Day && i < signUpBonusJson.Count)
                            {
                                signUpBonus = signUpBonusJson[i].Price;
                                i++;
                            }
                        }
                    }
                }

                //List xe trong slot
                var jobCar = _unitOfWork.JobsRepository.FindBy(x => x.SlotInCharge == slotId && x.JobStatus == JobStatusEnum.DONE.ToString()).Select(x => x.CarId).ToList();

                float? moldBonus = 0;
                //thuong theo so luong
                if (slot.NumberOfVehiclesReRegistered == jobCar.Count)
                {
                    var moldBonusJson = JsonSerializer.Deserialize<List<RuleRewardAndPunish>>(rule.MoldBonus);
                    if (moldBonusJson != null && moldBonusJson.Any())
                    {
                        var i = 0;
                        while (slot.NumberOfVehiclesReRegistered > moldBonusJson[i].Unit)
                        {
                            moldBonus = moldBonusJson[i].Price;
                            i++;
                        }
                    }
                }

                var carModel = new List<long?>();
                for (int i = 0; i < jobCar.Count; i++)
                {
                    carModel.Add(_unitOfWork.CarsRepository.GetSingle(x => x.Id == jobCar[i]).CarModelId);

                }

                var carType = new List<long?>();
                for (int i = 0; i < carModel.Count; i++)
                {
                    carType.Add(_unitOfWork.CarModelsRepository.GetSingle(x => x.Id == carModel[i]).ClassId);
                }

                //tinh luong
                decimal? salaryTotal = 0;
                for (int i = 0; i < carType.Count; i++)
                {
                    switch (carType[i])
                    {
                        case 1:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorA ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 2:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorB ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 3:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorC ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 4:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorD ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 5:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorE ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 6:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorF ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 7:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorM ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;
                        case 8:
                            salaryTotal += (rule.LaborWages ?? 0) * (rule.VehicleSizeFactorS ?? 1) * (rule.WeatherCoefficient ?? 1) * (rule.ContingencyCoefficient ?? 1);
                            break;

                    }
                }

                //phat
                float penalty = 0;
                if (jobCar.Count == 0)
                {
                    var penaltyJson = JsonSerializer.Deserialize<CancellationOfSchedulePenalty>(rule.CancellationOfSchedulePenalty);
                    penalty = penaltyJson.QuitWorkingPernalty.Value;
                }

                slot.TotalAmount = salaryTotal + (decimal?)(moldBonus + signUpBonus) - (decimal?)penalty - pileRegistration + slot.UnexpectedBonus - slot.UnexpectedPenalty;
                _unitOfWork.SlotsRepository.Update(slot);
                var result = await _unitOfWork.SlotsRepository.CommitAsync().ConfigureAwait(false);
                if (result)
                {
                    salary = new BonusSalary
                    {
                        MoldBonus = (decimal?)moldBonus,
                        SignUpBonus = (decimal?)signUpBonus,
                        CancellationOfSchedulePenalty = (decimal?)penalty,
                        TotalAmount = slot.TotalAmount
                    };
                    return new PagedResult<BonusSalary>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.UpdateSlotSuccess,
                        Data = salary
                    };
                }
                return new PagedResult<BonusSalary>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.UpdateSlotFail,
                    Data = salary
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionInternAsync(long slotId, string searchText)
        {
            try
            {
                var slot = GetSlots(slotId);

                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText ?? "");
                p.Add(Constants.WorkerType, (int)WorkerType.INTERN);
                p.Add(Constants.TeamId, slot.TeamId);
                p.Add(Constants.Day, slot.Day.Value.Date);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstSlots = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspSlots_SuggestionIntern", p);


                if (null == lstSlots)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstSlots.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionInChargeAsync(long workerId, DateTime bookJobDate, string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText ?? "");
                p.Add(Constants.WorkerId, workerId);
                p.Add(Constants.Day, bookJobDate.Date);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstSlots = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspSlots_SuggestionInCharge", p);


                if (null == lstSlots)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstSlots.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                throw;
            }
        }


    }
}
