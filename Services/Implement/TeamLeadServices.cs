using Common;
using Common.Constants;
using Common.Extentions;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Repository;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel.Jobs;
using ViewModel.RequestModel.Slots;
using ViewModel.ResponseMessage;

namespace Services.Implement
{
    public class TeamLeadServices : ITeamLeadServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public TeamLeadServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _authenticatedUser = authenticatedUser;
        }

        private List<BookSlotList> GetBookSlotListByWorkerMutipleDay(DateTime date, long workerId)
        {
            var result = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == workerId && x.Day <= date)?.OrderByDescending(x => x.Day).Select(
                    x => new BookSlotList
                    {
                        SlotId = x.Id,
                        Day = x.Day,
                        Start = x.TimeToCome,
                        End = x.TimeToGoHome,
                        BookStatus = x.BookStatus,
                        NumberOfVehicle = x.NumberOfVehiclesReRegistered ?? x.NumberOfRegisteredVehicles.Value,
                        TotalAmount = x.TotalAmount ?? 0
                    });
            return result.ToList();
        }

        //Worker 1.0
        public IPagedResults<BookSlotList> GetBookSlotList(DateTime? date = null)
        {
            try
            {
                var worker = _unitOfWork.WorkersRepository.GetSingle(x => x.UserId == _authenticatedUser.UserId);
                if (worker == null)
                {
                    return new PagedResults<BookSlotList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        ListData = null
                    };
                }

                if (date == null) date = DateTime.Now.Date;
                date = date.Value.AddDays(1);

                var query = GetBookSlotListByWorkerMutipleDay(date.Value, worker.Id);
                return new PagedResults<BookSlotList>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = query,
                    TotalRecords = query.Count()
                };


            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                throw;
            }
        }

        private BookSlotList GetBookSlotListByWorkerSingleDay(DateTime date, long workerId)
        {
            var result = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == workerId && x.Day.Value.Date == date.Date)?.Select(
                    x => new BookSlotList
                    {
                        SlotId = x.Id,
                        Day = x.Day,
                        Start = x.TimeToCome,
                        End = x.TimeToGoHome,
                        BookStatus = x.BookStatus,
                        NumberOfVehicle = x.NumberOfVehiclesReRegistered ?? x.NumberOfRegisteredVehicles.Value,
                        TotalAmount = x.TotalAmount ?? 0
                    }).FirstOrDefault();
            return result;
        }

        //TeamLead 1.0
        public IPagedResults<BookSlotList> GetBookSlotListByTeamLead(DateTime? date)
        {
            try
            {
                var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
                if (worker == null)
                {
                    return new PagedResults<BookSlotList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        ListData = null
                    };
                }

                if (worker.WorkerType == (int)WorkerType.TEAMLEAD)
                {
                    return new PagedResults<BookSlotList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Forbidden),
                        ResponseMessage = Constants.ErrorMessageCodes.TeamLeadPermissionGetList,
                        ListData = null
                    };
                }

                if (date == null) date = DateTime.Now.Date;


                var p = new DynamicParameters();
                p.Add(Constants.WorkerId, worker.Id);

                var query = @"select distinct w.id
                                from dbo.TeamLead tl join dbo.Workers w on tl.workerId = w.id
                                where tl.WorkerId = @WorkerId";
                var listWorker = _dapperRepository.QueryMultipleWithParam<long>(query, p).ToList();
                var listSlot = new List<BookSlotList>();
                for (int i = 0; i < listWorker.Count; i++)
                {
                    var slot = GetBookSlotListByWorkerSingleDay(date.Value, listWorker[i]);
                    if (slot != null)
                    {
                        listSlot.Add(slot);
                    }

                }

                return new PagedResults<BookSlotList>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = listSlot,
                    TotalRecords = listSlot.Count
                };


            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " SlotsServices: " + ex.Message);
                throw;
            }
        }

        //Worker 3.0 - 4.0
        public IPagedResults<BookJobList> GetJobList()
        {
            try
            {
                var worker = _unitOfWork.WorkersRepository.GetSingle(x => x.UserId == _authenticatedUser.UserId);
                if (worker == null)
                {
                    return new PagedResults<BookJobList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        ListData = null
                    };
                }

                var slot = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == worker.Id && x.Day.Value.Date == DateTime.Now.Date).FirstOrDefault();
                if (slot == null)
                {
                    return new PagedResults<BookJobList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.SlotsNotFoundMessage,
                        ListData = null
                    };
                }

                var p = new DynamicParameters();
                p.Add(Constants.SlotId, slot.Id);
                var query = @"select c.LicensePlates as LicensePlates, j.StartingTime as StartingTime, j.EndTime as EndTime , j.JobStatus as JobStatus
                                from dbo.Jobs j join dbo.cars c on j.carid = c.id 
                                where j.slotincharge = @SlotId";
                var lstJobs = _dapperRepository.QueryMultipleWithParam<BookJobList>(query, p);
                return new PagedResults<BookJobList>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = lstJobs.Count()
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                throw;
            }
        }

        //Worker 2.0
        public IPagedResults<GetJobBySlotModel> GetJobBySlot(string status = null)
        {
            var worker = _unitOfWork.WorkersRepository.GetSingle(x => x.UserId == _authenticatedUser.UserId);
            if (worker == null)
            {
                return new PagedResults<GetJobBySlotModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                    ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                    ListData = null
                };
            }

            // var slot = await _unitOfWork.SlotsRepository.GetSingleAsync(x => x.WorkerId == worker.Id && x.Day.Value.Date == DateTime.Now.Date && x.BookStatus == StatusEnum.Working.GetDescription());
            var slot = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == worker.Id && x.BookStatus == StatusEnum.Working.GetDescription()).OrderBy(x => x.Day).Where(x => x.Day.Value.Date == DateTime.Now.Date).FirstOrDefault();
            if (slot == null)
            {
                return new PagedResults<GetJobBySlotModel>
                {
                    ResponseCode = Constants.ErrorMessageCodes.RecordNotFound,
                    ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage
                };
            }

            var query = "";
            var dynamicParameterPlaceId = new DynamicParameters();
            dynamicParameterPlaceId.Add("@SlotId", slot.Id);
            if (status == null)
            {
                query = @"SELECT j.Id, c.LicensePlates, j.BookJobDate, j.Title, j.JobStatus, z.Title as Zone, c.Title as Column, t.Title as Team 
                              FROM     dbo.Jobs j INNER JOIN
                              dbo.ZoneColumn zc ON j.ColumnId = zc.ColumnId INNER JOIN
                              dbo.Cars c On j.carId = c.Id  INNER JOIN
                              dbo.Zones z ON  zc.ZoneId = z.Id INNER JOIN
                              dbo.TeamZone tz ON z.Id = tz.ZoneId INNER JOIN
                              dbo.Teams t ON tz.TeamId = t.Id INNER JOIN
                              dbo.Slots s ON j.slotincharge = s.id INNER JOIN
                              dbo.Columns c ON c.Id = zc.ColumnId 
                                where s.Id = @SlotId
                                and CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), GETDATE(), 103)";

            }
            else
            {
                dynamicParameterPlaceId.Add("@Status", status);
                query = @"SELECT j.Id, c.LicensePlates, j.BookJobDate, j.Title, j.JobStatus, z.Title as Zone, c.Title as Column, t.Title as Team 
                              FROM     dbo.Jobs j INNER JOIN
                              dbo.ZoneColumn zc ON j.ColumnId = zc.ColumnId INNER JOIN
                              dbo.Cars c On j.carId = c.Id  INNER JOIN
                              dbo.Zones z ON  zc.ZoneId = z.Id INNER JOIN
                              dbo.TeamZone tz ON z.Id = tz.ZoneId INNER JOIN
                              dbo.Teams t ON tz.TeamId = t.Id INNER JOIN
                              dbo.Slots s ON j.slotincharge = s.id INNER JOIN
                              dbo.Columns c ON c.Id = zc.ColumnId 
                                where s.Id = @SlotId
                                and CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), GETDATE(), 103)
                                and j.JobStatus = @Status";
            }


            var listInfoPlace = _dapperRepository.QueryMultipleWithParam<GetJobBySlotModel>(query, dynamicParameterPlaceId);
            listInfoPlace = listInfoPlace.DistinctBy(x => x.Id, null);
            listInfoPlace = listInfoPlace.OrderBy(x =>
            x.JobStatus == JobStatusEnum.TODO.ToString() ? 1 :
            x.JobStatus == JobStatusEnum.BOOKED.ToString() ? 2 :
            x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString() ? 3 :
            x.JobStatus == JobStatusEnum.DONE.ToString() ? 4 : 5).ThenByDescending(x => x.BookJobDate);
            return new PagedResults<GetJobBySlotModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                ListData = listInfoPlace,
                TotalRecords = listInfoPlace.Count()
            };
        }

        //TeamLead 2.0 getList
        public IPagedResults<GetJobBySlotModel> GetJobBySlotAsyncByTeamLead(long? workerId = null, string status = null)
        {
            var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
            if (worker == null)
            {
                return new PagedResults<GetJobBySlotModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                    ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                    ListData = null
                };
            }

            if (worker.WorkerType == (int)WorkerType.TEAMLEAD)
            {
                return new PagedResults<GetJobBySlotModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.Forbidden),
                    ResponseMessage = Constants.ErrorMessageCodes.TeamLeadPermissionGetList,
                    ListData = null
                };
            }

            var slot = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == workerId)?.Select(x => x.Id).ToList();

            var listSlotId = new List<long>();
            if (workerId == null)
            {
                var p = new DynamicParameters();
                var team = _unitOfWork.TeamLeadRepository.GetSingle(x => x.WorkerId == worker.Id);
                p.Add(Constants.WorkerId, team.TeamId);
                var queryGetSlot = @"select distinct s.id
                                    from dbo.Workers w join dbo.teamworker tw on w.id = tw.workerid
                                    join dbo.Slots s on tw.teamId = s.teamId
                                    where s.teamId = @TeamId ";
                var slotList = _dapperRepository.QueryMultipleWithParam<long>(queryGetSlot, p);
                slot = _unitOfWork.SlotsRepository.FindBy(x => slotList.Contains(x.Id)).Where(x => x.Day.Value.Date == DateTime.Now.Date).Select(x => x.Id).ToList();
            }
            else
            {

                if (slot == null)
                {
                    return new PagedResults<GetJobBySlotModel>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.RecordNotFound,
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage
                    };
                }
            }


            var query = "";
            var dynamicParameterPlaceId = new DynamicParameters();
            dynamicParameterPlaceId.Add("@SlotId", slot);
            if (status == null)
            {
                query = @"SELECT j.Id, c.LicensePlates, j.BookJobDate, j.Title, j.JobStatus
                              FROM     dbo.Jobs j INNER JOIN
                              dbo.ZoneColumn zc ON j.ColumnId = zc.ColumnId INNER JOIN
                              dbo.Cars c On j.carId = c.Id  INNER JOIN
                              dbo.Zones z ON  zc.ZoneId = z.Id INNER JOIN
                              dbo.TeamZone tz ON z.Id = tz.ZoneId INNER JOIN
                              dbo.Teams t ON tz.TeamId = t.Id INNER JOIN
                                dbo.Slots s ON j.slotincharge = s.id
                                where s.Id IN @SlotId
                                and CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), GETDATE(), 103)";

            }
            else
            {
                dynamicParameterPlaceId.Add("@Status", status);
                query = @"SELECT j.Id, c.LicensePlates, j.BookJobDate, j.Title, j.JobStatus
                              FROM     dbo.Jobs j INNER JOIN
                              dbo.ZoneColumn zc ON j.ColumnId = zc.ColumnId INNER JOIN
                              dbo.Cars c On j.carId = c.Id  INNER JOIN
                              dbo.Zones z ON  zc.ZoneId = z.Id INNER JOIN
                              dbo.TeamZone tz ON z.Id = tz.ZoneId INNER JOIN
                              dbo.Teams t ON tz.TeamId = t.Id INNER JOIN
                               dbo.Slots s ON j.slotincharge = s.id
                                where s.Id IN @SlotId
                                and CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), GETDATE(), 103)
                                and j.JobStatus = @Status";
            }


            var listInfoPlace = _dapperRepository.QueryMultipleWithParam<GetJobBySlotModel>(query, dynamicParameterPlaceId);
            listInfoPlace = listInfoPlace.DistinctBy(x => x.Id, null);
            listInfoPlace = listInfoPlace.OrderBy(x =>
            x.JobStatus == JobStatusEnum.TODO.ToString() ? 1 :
            x.JobStatus == JobStatusEnum.BOOKED.ToString() ? 2 :
            x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString() ? 3 :
            x.JobStatus == JobStatusEnum.DONE.ToString() ? 4 : 5).ThenByDescending(x => x.BookJobDate);
            return new PagedResults<GetJobBySlotModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                ListData = listInfoPlace,
                TotalRecords = listInfoPlace.Count()
            };
        }

        //TeamLead 2.0 Assessment
        public async Task<IPagedResult<bool>> TeamLeadCommentsAsync(CommentModel model)
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

                if (worker.WorkerType == (int)WorkerType.TEAMLEAD)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Forbidden),
                        ResponseMessage = Constants.ErrorMessageCodes.TeamLeadPermissionGetList,
                        Data = false
                    };
                }

                var entity = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == model.JobId);
                if (null == entity)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                        ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                    };
                }

                entity.TeamLeadAssessment = model.Comments;
                entity.TeamLeadScore = model.Score;
                _unitOfWork.JobsRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.UpdateSuccessMessage,
                    Data = true
                }; ;
            }
            catch (Exception e)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Team Lead Comment Error: " + e.Message);
                throw;
            }
        }

        //TeamLead 3.0 TimeLine-Report
        public PagedResults<TimeLineReportModel> TimeLineReportAsync(DateTime? dateTime)
        {
            var teamLead = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
            if (teamLead == null)
            {
                return new PagedResults<TimeLineReportModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                    ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                    ListData = null
                };
            }

            if (teamLead.WorkerType == (int)WorkerType.TEAMLEAD)
            {
                return new PagedResults<TimeLineReportModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.Forbidden),
                    ResponseMessage = Constants.ErrorMessageCodes.TeamLeadPermissionGetList,
                    ListData = null

                };
            }

            var team = _unitOfWork.TeamLeadRepository.GetSingle(x => x.WorkerId == teamLead.Id);

            var prm = new DynamicParameters();
            prm.Add(Constants.TeamId, team.TeamId);
            prm.Add(Constants.Day, dateTime?.Date ?? DateTime.Now);
            var query = @"SELECT u.FullName, j.Title, j.BookJobDate, j.JobStatus, c.[LicensePlates] , j.StartingTime, j.EndTime
                  FROM dbo.Slots s 
                  INNER JOIN dbo.Workers w ON s.WorkerId = w.Id AND w.IsDeleted = 0
                  INNER JOIN dbo.Jobs j ON s.Id = j.SlotInCharge AND j.IsDeleted = 0
                  INNER JOIN dbo.UserProfile u ON w.UserId = u.Id AND u.IsDeleted = 0
                  INNER JOIN dbo.Cars c ON j.CarId = c.Id AND c.IsDeleted = 0
                  WHERE CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), @day, 103) AND s.IsDeleted = 0
                  AND s.TeamId = @TeamId";
            var infoBasement = _dapperRepository.QueryMultipleWithParam<JobTimlineDapper>(query, prm);
            var result = infoBasement.Select(x => new TimeLineReportModel
            {
                WorkerName = x.FullName,
            }).DistinctBy(y => y.WorkerName, null).ToList();
            result.ForEach(x =>
            {
                x.CarsBooked = infoBasement.Count(y => y.FullName == x.WorkerName);
                x.Jobs = infoBasement.Where(y => y.FullName == x.WorkerName).Select(z => new JobInformation
                {
                    BookJobDate = z.BookJobDate,
                    EndTime = z.EndTime,
                    JobStatus = z.JobStatus,
                    LicensePlates = z.LicensePlates,
                    StartingTime = z.StartingTime
                }).OrderBy(x =>
                x.JobStatus == JobStatusEnum.TODO.ToString() ? 1 :
                x.JobStatus == JobStatusEnum.BOOKED.ToString() ? 2 :
                x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString() ? 3 :
                x.JobStatus == JobStatusEnum.DONE.ToString() ? 4 : 5).ThenByDescending(x => x.BookJobDate);
            });
            return new PagedResults<TimeLineReportModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                ListData = result,
                TotalRecords = result.Count
            };
        }

        //TeamLead 4.0 List Job worker
        public async Task<IPagedResults<BookJobList>> GetJobListByTeamLead(long workerId)
        {
            try
            {

                var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
                if (worker == null)
                {
                    return new PagedResults<BookJobList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        ListData = null
                    };
                }

                if (worker.WorkerType == (int)WorkerType.TEAMLEAD)
                {
                    return new PagedResults<BookJobList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Forbidden),
                        ResponseMessage = Constants.ErrorMessageCodes.TeamLeadPermissionGetList,
                        ListData = null

                    };
                }

                var team = await _unitOfWork.TeamLeadRepository.GetSingleAsync(x => x.WorkerId == worker.Id);

                var slot = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == workerId && x.Day.Value.Date == DateTime.Now.Date && team.TeamId == x.TeamId).FirstOrDefault();
                if (slot == null)
                {
                    return new PagedResults<BookJobList>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.SlotsNotFoundMessage,
                        ListData = null
                    };
                }

                var p = new DynamicParameters();
                p.Add(Constants.SlotId, slot.Id);
                var query = @"select c.LicensePlates as LicensePlates, j.StartingTime as StartingTime, j.EndTime as EndTime , j.JobStatus as JobStatus
                                from dbo.Jobs j join dbo.cars c on j.carid = c.id 
                                where j.slotincharge = @SlotId";
                var lstJobs = _dapperRepository.QueryMultipleWithParam<BookJobList>(query, p);
                return new PagedResults<BookJobList>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = lstJobs.Count()
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                throw;
            }
        }

        //Get Worker In Team
        public async Task<IPagedResults<ItemModel>> GetWorkerInTeam()
        {

            var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
            if (worker == null)
            {
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                    ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                    ListData = null
                };
            }

            if (worker.WorkerType == (int)WorkerType.TEAMLEAD)
            {
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.Forbidden),
                    ResponseMessage = Constants.ErrorMessageCodes.TeamLeadPermissionGetList,
                    ListData = null

                };
            }

            var team = await _unitOfWork.TeamLeadRepository.GetSingleAsync(x => x.WorkerId == worker.Id);
            var p = new DynamicParameters();
            p.Add(Constants.TeamId, team.TeamId);

            var query = @"select w.id as Id, u.UserName as Value
                        from dbo.workers w join dbo.teamworker tw on w.id = tw.workerid
                        join dbo.UserProfile u on w.UserId = u.Id
                        where tw.teamid= @TeamId";
            var listWorker = _dapperRepository.QueryMultipleWithParam<ItemModel>(query, p);
            return new PagedResults<ItemModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                ListData = listWorker,
                TotalRecords = listWorker.Count()
            };
        }

        //WorkerUpdate Slot
        public async Task<IPagedResult<bool>> UpdateSlotAsync(WorkerUpdateSlot updateSlot)
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

          
                slot.NumberOfVehiclesReRegistered = updateSlot.NumberOfVehiclesReRegistered ?? slot.NumberOfRegisteredVehicles;
               

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

        //WorkerBookJob
        public async Task<IPagedResult<bool>> BookJobAsync(long jobId)
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


                var old = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == jobId && x.JobStatus == JobStatusEnum.TODO.ToString());
                if (old == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.JobExisted,
                        ResponseMessage = Constants.ErrorMessageCodes.JobHasCheckedMessage,
                    };
                }
                old.JobStatus = JobStatusEnum.BOOKED.ToString();
                old.SlotInCharge = slot.Id;
                _unitOfWork.JobsRepository.Update(old);
                var id = await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    Data = true,
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Book Job Error: " + ex.Message);
                throw;
            }
        }

        //TeamLead book job
        public async Task<IPagedResult<bool>> BookJobAsyncByTeamLead(long jobId, long workerId)
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

                if (worker.WorkerType == (int)WorkerType.TEAMLEAD)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                        Data = false
                    };
                }

                var slot = _unitOfWork.SlotsRepository.GetSingle(x => x.WorkerId == workerId && x.Day.Value.Date == DateTime.Now.Date);
              
                if (slot == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.NotExistedMessage,
                        Data = false
                    };
                }


                var old = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == jobId && x.JobStatus == JobStatusEnum.TODO.ToString());
                if (old == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.JobExisted,
                        ResponseMessage = Constants.ErrorMessageCodes.JobHasCheckedMessage,
                    };
                }
                old.JobStatus = JobStatusEnum.BOOKED.ToString();
                old.SlotInCharge = slot.Id;
                _unitOfWork.JobsRepository.Update(old);
                var id = await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    Data = true,
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Book Job Error: " + ex.Message);
                throw;
            }
        }

        //Worker TimeLine
        //TeamLead 3.0 TimeLine-Report
        public IPagedResults<TimeLineReportModel> TimeLineReportAsyncWorker(DateTime? dateTime)
        {
            var worker = _unitOfWork.WorkersRepository.FindBy(x => x.UserId == _authenticatedUser.UserId).FirstOrDefault();
            if (worker == null)
            {
                return new PagedResults<TimeLineReportModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                    ResponseMessage = Constants.ErrorMessageCodes.UserNotFound,
                    ListData = null
                };
            }

           

           

            var prm = new DynamicParameters();
            prm.Add(Constants.WorkerId, worker.Id);
            prm.Add(Constants.Day, dateTime?.Date ?? DateTime.Now);
            var query = @"SELECT u.FullName, j.Title, j.BookJobDate, j.JobStatus, c.[LicensePlates] , j.StartingTime, j.EndTime
                  FROM dbo.Slots s 
                  INNER JOIN dbo.Workers w ON s.WorkerId = w.Id AND w.IsDeleted = 0
                  INNER JOIN dbo.Jobs j ON s.Id = j.SlotInCharge AND j.IsDeleted = 0
                  INNER JOIN dbo.UserProfile u ON w.UserId = u.Id AND u.IsDeleted = 0
                  INNER JOIN dbo.Cars c ON j.CarId = c.Id AND c.IsDeleted = 0
                  WHERE CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), @day, 103) AND s.IsDeleted = 0
                  AND s.WorkerId = @WorkerId";
            var infoBasement = _dapperRepository.QueryMultipleWithParam<JobTimlineDapper>(query, prm);
            var result = infoBasement.Select(x => new TimeLineReportModel
            {
                WorkerName = x.FullName,
            }).DistinctBy(y => y.WorkerName, null).ToList();
            result.ForEach(x =>
            {
                x.CarsBooked = infoBasement.Count(y => y.FullName == x.WorkerName);
                x.Jobs = infoBasement.Where(y => y.FullName == x.WorkerName).Select(z => new JobInformation
                {
                    BookJobDate = z.BookJobDate,
                    EndTime = z.EndTime,
                    JobStatus = z.JobStatus,
                    LicensePlates = z.LicensePlates,
                    StartingTime = z.StartingTime
                }).OrderBy(x =>
                x.JobStatus == JobStatusEnum.TODO.ToString() ? 1 :
                x.JobStatus == JobStatusEnum.BOOKED.ToString() ? 2 :
                x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString() ? 3 :
                x.JobStatus == JobStatusEnum.DONE.ToString() ? 4 : 5).ThenByDescending(x => x.BookJobDate);
            });
            return new PagedResults<TimeLineReportModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                ListData = result,
                TotalRecords = result.Count
            };
        }
    }
}
