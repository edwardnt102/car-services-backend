using ClosedXML.Excel;
using Common;
using Common.Constants;
using Common.Extentions;
using Common.Pagging;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Hosting;
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
using ViewModel.Report;
using ViewModel.RequestModel;
using ViewModel.ResponseMessage;
using ViewModel.ViewModels;
// ReSharper disable UseMethodAny.0
// ReSharper disable ReplaceWithSingleCallToCount

namespace Services.Implement
{
    public class ReportServices : IReportServices
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private static string _webRootPath = string.Empty;
        public ReportServices(IDapperRepository dapperRepository, IUnitOfWork unitOfWork, IOptions<AppSettings> options, IHostingEnvironment hostingEnvironment)
        {
            _dapperRepository = dapperRepository;
            _unitOfWork = unitOfWork;
            _appSettings = options.Value;
            _webRootPath = hostingEnvironment.WebRootPath + Constants.Upload;
        }

        public IEnumerable<TimeLineReportModel> TimeLineReportAsync(DateTime? dateTime)
        {
            var prm = new DynamicParameters();
            prm.Add(Constants.Day, dateTime?.Date ?? DateTime.Now);
            var query = @"SELECT u.FullName, j.Title, j.BookJobDate, j.JobStatus, c.[LicensePlates] , j.StartingTime, j.EndTime
                  FROM dbo.Slots s 
                  INNER JOIN dbo.Workers w ON s.WorkerId = w.Id AND w.IsDeleted = 0
                  INNER JOIN dbo.Jobs j ON s.Id = j.SlotInCharge AND j.IsDeleted = 0
                  INNER JOIN dbo.UserProfile u ON w.UserId = u.Id AND u.IsDeleted = 0
                  INNER JOIN dbo.Cars c ON j.CarId = c.Id AND c.IsDeleted = 0
                  WHERE CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), @day, 103) AND s.IsDeleted = 0";
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
                });
            });
            return result;
        }

        public IPagedResult<CoordinatorViewModel> CoordinatorAsync(long placeId, DateTime? day)
        {
            try
            {
                day ??= DateTime.Now;

                // call sql
                var dynamicParameterPlaceId = new DynamicParameters();
                dynamicParameterPlaceId.Add(Constants.PlaceId, placeId);

                var dynamicParameterPlace = new DynamicParameters();
                dynamicParameterPlace.Add(Constants.PlaceId, placeId);
                dynamicParameterPlace.Add(Constants.Day, day);

                var queryInfoPlace = @"SELECT dbo.Places.Title AS PlaceName, dbo.Rules.Title AS RuleName FROM dbo.Places LEFT JOIN dbo.Rules ON dbo.Places.RuleId = dbo.Rules.Id AND dbo.Rules.IsDeleted = 0 WHERE dbo.Places.Id = @PlaceId AND dbo.Places.IsDeleted = 0";
                var listInfoPlace = _dapperRepository.QueryMultipleWithParam<InfoPlaceReport>(queryInfoPlace, dynamicParameterPlaceId);

                var queryInfoBasement = @"SELECT Title AS BasementName FROM dbo.Basements WHERE dbo.Basements.PlaceId = @PlaceId AND dbo.Basements.IsDeleted = 0";
                var infoBasement = _dapperRepository.QueryMultipleWithParam<BasementReport>(queryInfoBasement, dynamicParameterPlaceId);
                var listInfoBasement = infoBasement.Select(x => x.BasementName);

                var queryInfoJob = @"SELECT dbo.Jobs.Id, dbo.Jobs.BookJobDate, dbo.Jobs.ColumnId, dbo.Jobs.SlotInCharge, dbo.Jobs.JobStatus FROM dbo.Jobs INNER JOIN dbo.Columns ON dbo.Jobs.ColumnId = dbo.Columns.Id AND dbo.Columns.IsDeleted = 0 INNER JOIN dbo.Basements ON dbo.Columns.BasementId = dbo.Basements.Id AND dbo.Basements.IsDeleted = 0 INNER JOIN dbo.Places ON dbo.Basements.PlaceId = dbo.Places.Id AND dbo.Places.IsDeleted = 0 WHERE dbo.Places.Id = @PlaceId AND CONVERT(VARCHAR(10),dbo.Jobs.BookJobDate, 103) = CONVERT(VARCHAR(10), @day, 103) AND dbo.Jobs.IsDeleted = 0";
                var listInfoJob = _dapperRepository.QueryMultipleWithParam<JobReport>(queryInfoJob, dynamicParameterPlace);

                var queryInfoSub = @"SELECT dbo.Subscriptions.Id, dbo.Subscriptions.Title AS SubscriptionName FROM dbo.Subscriptions INNER JOIN dbo.Cars ON dbo.Subscriptions.CarId = dbo.Cars.Id AND dbo.Cars.IsDeleted = 0 INNER JOIN dbo.Customers ON dbo.Cars.CustomerId = dbo.Customers.Id AND dbo.Customers.IsDeleted = 0 INNER JOIN dbo.Places ON dbo.Customers.PlaceId = dbo.Places.Id AND dbo.Places.IsDeleted = 0 WHERE dbo.Places.Id = @PlaceId AND dbo.Subscriptions.IsDeleted = 0";
                var listInfoSub = _dapperRepository.QueryMultipleWithParam<SubscriptionReport>(queryInfoSub, dynamicParameterPlaceId);

                var queryInfoTeam = @"SELECT dbo.TeamPlaces.Id, dbo.TeamPlaces.TeamId, dbo.TeamPlaces.PlaceId FROM dbo.TeamPlaces WHERE dbo.TeamPlaces.PlaceId = @PlaceId";
                var listInfoTeam = _dapperRepository.QueryMultipleWithParam<TeamPlaces>(queryInfoTeam, dynamicParameterPlaceId);

                var lstTeam = listInfoTeam.Count() > 0 ? string.Join(",", listInfoTeam.Select(x => x.TeamId)) : 0.ToString();
                var queryInfoTeamWorker = $"SELECT DISTINCT dbo.TeamWorker.WorkerId FROM dbo.TeamWorker WHERE dbo.TeamWorker.TeamId IN ({lstTeam})";
                var listInfoTeamWorker = _dapperRepository.QueryMultiple<TeamWorker>(queryInfoTeamWorker);

                var queryInfoSlot = @"SELECT dbo.Slots.Id, dbo.Slots.Title AS SlotName, dbo.Slots.WorkerId, dbo.Slots.TeamId, dbo.Slots.BookStatus, dbo.Slots.CheckInTime, dbo.Slots.NumberOfRegisteredVehicles FROM dbo.Slots INNER JOIN dbo.Places ON dbo.Slots.PlaceId = dbo.Places.Id AND dbo.Places.IsDeleted = 0 WHERE dbo.Slots.IsDeleted = 0 AND dbo.Places.Id = @PlaceId AND CONVERT(VARCHAR(10),dbo.Slots.Day, 103) = CONVERT(VARCHAR(10), @day, 103)";
                var listInfoSlot = _dapperRepository.QueryMultipleWithParam<SlotReport>(queryInfoSlot, dynamicParameterPlace);

                // model
                var placeName = listInfoPlace.FirstOrDefault()?.PlaceName ?? string.Empty;
                var basementName = listInfoBasement.Count() > 0 ? string.Join(" ", listInfoBasement) : 0.ToString();
                var ruleName = listInfoPlace.FirstOrDefault()?.RuleName ?? string.Empty;
                var carApproved = listInfoSlot.Where(x => x.BookStatus == StatusEnum.Approved.GetDescription());
                var checkInTime = listInfoSlot.Where(x => x.BookStatus == StatusEnum.Approved.GetDescription() && x.CheckInTime != null);

                var numberOfRegisteredVehicles = 0;
                if (carApproved.Count() > 0)
                {
                    numberOfRegisteredVehicles += carApproved.Sum(item => item.NumberOfRegisteredVehicles ?? 0);
                }

                var carCheckIn = 0;
                if (checkInTime.Count() > 0)
                {
                    carCheckIn += checkInTime.Sum(item => item.NumberOfRegisteredVehicles ?? 0);
                }
                var inProgress = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString()).Count();
                var booked = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.BOOKED.ToString()).Count();
                var done = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.DONE.ToString()).Count();
                var todo = carCheckIn - inProgress - booked - done;

                var queryInfoZone = @"SELECT dbo.Zones.Id, dbo.Zones.Title AS ZoneName,dbo.Zones.ColorCode FROM dbo.Zones WHERE dbo.Zones.PlaceId = @PlaceId AND dbo.Zones.IsDeleted = 0";
                var listInfoZone = _dapperRepository.QueryMultipleWithParam<ZoneReport>(queryInfoZone, dynamicParameterPlaceId);

                var jobCar = listInfoJob.Where(x => x.ColumnId != null && x.ColumnId > 0);
                var zoneHasJobTodo = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.TODO.ToString()).Count();
                var zoneHasJobInProgress = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString()).Count();
                var zoneHasJobBooked = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.BOOKED.ToString()).Count();
                var zoneHasJobDone = listInfoJob.Where(x => x.JobStatus == JobStatusEnum.DONE.ToString()).Count();

                var listTeam = new List<ListTeamViewModel>();
                var queryInfoTeamPlaces = @"SELECT dbo.Teams.Title AS TeamName, dbo.TeamPlaces.TeamId, dbo.Teams.ColorCode FROM dbo.TeamPlaces INNER JOIN dbo.Teams ON dbo.TeamPlaces.TeamId = dbo.Teams.Id AND dbo.Teams.IsDeleted = 0 WHERE dbo.TeamPlaces.PlaceId = @PlaceId";
                var listInfoTeamPlaces = _dapperRepository.QueryMultipleWithParam<TeamPlaceReport>(queryInfoTeamPlaces, dynamicParameterPlaceId);
                if (listInfoTeamPlaces.Count() > 0)
                {
                    var queryInfoTeamWorkers = $"SELECT dbo.TeamWorker.TeamId, dbo.TeamWorker.WorkerId FROM dbo.TeamWorker WHERE dbo.TeamWorker.TeamId IN ({string.Join(", ", listInfoTeamPlaces.Select(x => x.TeamId))})";
                    var listInfoTeamWorkers = _dapperRepository.QueryMultiple<TeamWorkerReport>(queryInfoTeamWorkers);
                    var jobTeam = from a in listInfoJob
                                  join b in listInfoSlot on a.SlotInCharge equals b.Id
                                  select new JobTeamReport
                                  {
                                      TeamId = b.TeamId,
                                      JobStatus = a.JobStatus
                                  };

                    foreach (var item in listInfoTeamPlaces)
                    {
                        var totalInfoTeamWorkers = listInfoTeamWorkers.Where(x => x.TeamId == item.TeamId).Count();
                        var totalInfoSlots = listInfoSlot.Where(x => x.TeamId == item.TeamId).Count();
                        var totalJobTeam = jobTeam.Where(x => x.TeamId == item.TeamId).Count();
                        var totalCarApproved = carApproved.Where(x => x.TeamId == item.TeamId).Count();
                        var totalNumberCarApproved = carApproved.Where(x => x.TeamId == item.TeamId).Sum(x => x.NumberOfRegisteredVehicles ?? 0);
                        var totalCheckInTime = checkInTime.Where(x => x.TeamId == item.TeamId).Count();
                        var totalNumberCheckInTime = checkInTime.Where(x => x.TeamId == item.TeamId).Sum(x => x.NumberOfRegisteredVehicles ?? 0);

                        var inProgressTeam = jobTeam.Where(x => x.TeamId == item.TeamId && x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString()).Count();
                        var bookedTeam = jobTeam.Where(x => x.TeamId == item.TeamId && x.JobStatus == JobStatusEnum.BOOKED.ToString()).Count();
                        var doneTeam = jobTeam.Where(x => x.TeamId == item.TeamId && x.JobStatus == JobStatusEnum.DONE.ToString()).Count();
                        var todoTeam = totalNumberCheckInTime - inProgressTeam - bookedTeam - doneTeam;

                        var listTeamViewModel = new ListTeamViewModel
                        {
                            TeamId = item.TeamId,
                            TeamTotal = item.TeamName + "(" + totalInfoTeamWorkers + ")",//Đội (Quân số)
                            WorkerUnregisteredTotal = totalInfoTeamWorkers - totalInfoSlots, //Số lao động chưa đăng ký
                            SubscribeCalendar = totalInfoSlots + "(" + totalJobTeam + ")", // Đăng ký lịch (Số xe)
                            UnapprovedCarNumber = (totalInfoSlots - totalCarApproved) + "(" + (totalJobTeam - totalNumberCarApproved) + ")", // Chưa duyệt (Số xe)
                            CarApproved = totalCarApproved + "(" + totalNumberCarApproved + ")", // Đã duyệt (Số xe)
                            CarNotAttendance = (totalCarApproved - totalCheckInTime) + "(" + (totalNumberCarApproved - totalNumberCheckInTime) + ")", // Số xe chưa điểm danh
                            CarAttendance = totalCheckInTime + "(" + totalNumberCheckInTime + ")",// Điểm danh (Số xe)
                            JobStatus = todoTeam + "[" + inProgressTeam + "-" + bookedTeam + "-" + doneTeam + "]", // Còn [Đang làm-Đã book-Xong]
                            ColorCode = item.ColorCode
                        };
                        listTeam.Add(listTeamViewModel);
                    }
                }

                var listZone = new List<ListZoneViewModel>();
                if (listInfoZone.Count() > 0)
                {
                    var queryInfoTeamZone = $"SELECT dbo.TeamZone.ZoneId, dbo.TeamZone.TeamId FROM dbo.TeamZone WHERE dbo.TeamZone.ZoneId IN ({string.Join(", ", listInfoZone.Select(x => x.Id))})";
                    var listInfoTeamZone = _dapperRepository.QueryMultiple<TeamZoneReport>(queryInfoTeamZone);
                    if (listInfoTeamZone.Count() > 0)
                    {
                        var zoneTeam = from a in listInfoZone
                                       join b in listInfoTeamZone on a.Id equals b.ZoneId
                                       select new TeamZoneReport
                                       {
                                           ZoneName = a.ZoneName,
                                           TeamId = b.TeamId,
                                           ZoneId = b.ZoneId,
                                           ColorCode = a.ColorCode
                                       };

                        var jobZone = from a in jobCar
                                      join b in listInfoSlot on a.SlotInCharge equals b.Id
                                      select new JobTeamReport
                                      {
                                          TeamId = b.TeamId,
                                          JobStatus = a.JobStatus
                                      };

                        if (zoneTeam.Count() > 0)
                        {
                            foreach (var item in zoneTeam)
                            {
                                var zoneTeams = zoneTeam.Where(x => x.TeamId == item.TeamId).Count();
                                var inProgressZone = jobZone.Where(x => x.TeamId == item.TeamId && x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString()).Count();
                                var bookedZone = jobZone.Where(x => x.TeamId == item.TeamId && x.JobStatus == JobStatusEnum.BOOKED.ToString()).Count();
                                var doneZone = jobZone.Where(x => x.TeamId == item.TeamId && x.JobStatus == JobStatusEnum.DONE.ToString()).Count();
                                var todoZone = zoneTeams - inProgressZone - bookedZone - doneZone;

                                var listZoneViewModel = new ListZoneViewModel
                                {
                                    ZoneId = item.ZoneId,
                                    TeamId = item.TeamId,
                                    ZoneName = item.ZoneName + "(" + zoneTeams + ")",
                                    ZoneStatus = todoZone + "-" + inProgressZone + "-" + bookedZone + "-" + doneZone,
                                    ColorCode = item.ColorCode
                                };
                                listZone.Add(listZoneViewModel);
                            }
                        }
                    }
                }

                var result = new CoordinatorViewModel
                {
                    PlaceBasementName = placeName + " [" + basementName + "]", //Tên tòa nhà + hầm
                    CurrentDate = day?.ToString(Constants.ddMMyyyy), // Ngày
                    RuleName = "Rule : " + ruleName, // Rule
                    TotalJobSub = listInfoJob.Count() + " job / " + listInfoSub.Count() + " sub" + " (" + (listInfoSub.Count() - listInfoJob.Count()) + ")", // Job/Sub
                    TeamTotal = listInfoTeam.Count() + " Đội (" + listInfoTeamWorker.Count() + " lao động)", //Đội (Quân số)
                    WorkerUnregisteredTotal = (listInfoTeamWorker.Count() - listInfoSlot.Count()), //Số lao động chưa đăng ký
                    SubscribeCalendar = listInfoSlot.Count() + "(" + listInfoJob.Count() + ")", // Đăng ký lịch (Số xe)
                    UnapprovedCarNumber = (listInfoSlot.Count() - carApproved.Count()) + "(" + (listInfoJob.Count() - numberOfRegisteredVehicles) + ")", // Chưa duyệt (Số xe)
                    CarApproved = carApproved.Count() + "(" + numberOfRegisteredVehicles + ")", // Đã duyệt (Số xe)
                    CarNotAttendance = (carApproved.Count() - checkInTime.Count()) + "(" + (numberOfRegisteredVehicles - carCheckIn) + ")", // Số xe chưa điểm danh
                    CarAttendance = checkInTime.Count() + "(" + carCheckIn + ")",// Điểm danh (Số xe)
                    JobStatus = todo + "[" + inProgress + "-" + booked + "-" + done + "]", // Còn [Đang làm-Đã book-Xong]
                    ZoneCar = listInfoZone.Count() + " Zone (" + jobCar.Count() + " job)", // Khu vực (Số xe về)
                    Status = zoneHasJobTodo + "-" + zoneHasJobInProgress + "-" + zoneHasJobBooked + "-" + zoneHasJobDone,// Chưa book-Đang làm-Đã book-Xong
                    ListTeam = listTeam,
                    ListZone = listZone
                };

                return new PagedResult<CoordinatorViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        private List<BasementMap> GetDetailFromExcel(string diagramAttachmentNew)
        {
            using var workbook = new XLWorkbook(_webRootPath + Constants.Source + diagramAttachmentNew);
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.Range(worksheet.FirstCell(), worksheet.LastCellUsed());
            List<BasementMap> map = new List<BasementMap>();
            if (range == null) return map;
            for (var i = 1; i <= range.ColumnCount(); i++)
            {
                var columnName = "";
                var column = new List<ColumnsDetail>();
                for (var j = 1; j <= range.RowCount(); j++)
                {


                    if (!string.IsNullOrEmpty(worksheet.Cell(j, i).Value.ToString()) && !string.IsNullOrWhiteSpace(worksheet.Cell(j, i).Value.ToString()))
                    {
                        var color = "";
                        if (worksheet.Cell(j, i).Style.Fill.BackgroundColor.ColorType.ToString() == "Theme")
                        {
                            color = HexConverter(workbook.Theme.ResolveThemeColor(worksheet.Cell(j, i).Style.Fill.BackgroundColor.ThemeColor).Color);
                        }
                        else
                        {
                            color = HexConverter(worksheet.Cell(j, i).Style.Fill.BackgroundColor.Color);
                        }
                        column.Add(new ColumnsDetail
                        {
                            Title = worksheet.Cell(j, i).Value.ToString(),
                            Color = color
                        });
                        columnName = worksheet.Cell(j, i).Value.ToString().Substring(0, 1);


                    }
                    else
                    {
                        string color;
                        if (worksheet.Cell(j, i).Style.Fill.BackgroundColor.ColorType.ToString() != "Theme")
                        {
                            color = HexConverter(worksheet.Cell(j, i).Style.Fill.BackgroundColor.Color);
                        }
                        else
                        {
                            color = HexConverter(workbook.Theme
                                .ResolveThemeColor(worksheet.Cell(j, i).Style.Fill.BackgroundColor.ThemeColor).Color);
                        }

                        column.Add(new ColumnsDetail
                        {
                            Title = string.Empty,
                            Color = color
                        });
                    }
                }
                map.Add(new BasementMap
                {
                    ColumnName = columnName,
                    Columns = column
                });
            }
            return map;
        }

        private string HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public async Task<List<BasementDetail>> GetDetailAsync(long placeId)
        {
            var basements = _unitOfWork.BasementsRepository.FindBy(x => x.PlaceId == placeId).ToList();
            if (basements == null)
            {
                return null;
            }

            var result = new List<BasementDetail>();
            for (int i = 0; i < basements.Count; i++)
            {
                result.Add(new BasementDetail
                {
                    BasementId = basements[i].Id,
                    BasementName = basements[i].Title,
                    Columns = await GetBasementDetailAsync(basements[i].Id).ConfigureAwait(false)
                });
            }
            return result;

        }

        public async Task<List<BasementMap>> GetBasementDetailAsync(long basementId)
        {
            var place = await _unitOfWork.BasementsRepository.GetSingleAsync(x => x.Id == basementId);

            // khong tim thay basement
            if (place == null)
            {
                return null;
            }
            //lay ban ve basement

            try
            {
                var basementMap = GetDetailFromExcel(place.DiagramAttachmentReName);
                return basementMap;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }

        }

        public async Task<ColumnDetail> GetDetailByColumnAsync(long basementId, string columnName)
        {
            var place = await _unitOfWork.BasementsRepository.GetSingleAsync(x => x.Id == basementId);
            var basementMap = GetDetailsFromExcel(_webRootPath + Constants.Source + place.DiagramAttachmentReName);

            var detail = new ColumnDetail();
            for (var i = 0; i < basementMap.GetLength(0); i++)
            {
                for (var j = 0; j < basementMap.GetLength(1); j++)
                {
                    if (basementMap[i, j].Title != columnName) continue;
                    var columnId = _unitOfWork.ColumnsRepository.GetSingleAsync(x => x.BasementId == basementId && x.Title == columnName)?.Id;
                    if (columnId == null)
                    {
                        return new ColumnDetail
                        {
                            Column = basementMap[i, j]
                        };
                    }
                    var jobDetail = _unitOfWork.JobsRepository.FindBy(x => x.ColumnId == columnId && x.BookJobDate.Value.Date == DateTime.Now.Date).ToList();
                    var slot = jobDetail.Select(x => x.SlotInCharge).Distinct().ToList();
                    var worker = new List<InProgressWorker>();
                    foreach (var item in slot)
                    {
                        var slotDetail = _unitOfWork.SlotsRepository.GetSingle(x => x.Id == item).WorkerId;
                        var carProgress = jobDetail.Where(x => x.SlotInCharge == item && x.JobStatus == Constants.JobStatus.InProgress).Select(x => x.CarId);
                        var carBooked = jobDetail.Where(x => x.SlotInCharge == item && x.JobStatus == Constants.JobStatus.Booked).Select(x => x.CarId);
                        var carDone = jobDetail.Where(x => x.SlotInCharge == item && x.JobStatus == Constants.JobStatus.Done).Select(x => x.CarId);
                        worker.Add(new InProgressWorker
                        {
                            Name = _unitOfWork.WorkersRepository.GetSingle(x => slotDetail == x.Id)?.Title,
                            CarInProgress = _unitOfWork.CarsRepository.FindBy(x => carProgress.Contains(x.Id))?.Select(x => x.LicensePlates).ToList(),
                            CarBooked = _unitOfWork.CarsRepository.FindBy(x => carBooked.Contains(x.Id))?.Select(x => x.LicensePlates).ToList(),
                            CarDone = _unitOfWork.CarsRepository.FindBy(x => carDone.Contains(x.Id))?.Select(x => x.LicensePlates).ToList()
                        });
                    }
                    var carNotInclude = _unitOfWork.JobsRepository.FindBy(x => x.ColumnId != columnId && x.BookJobDate.Value.Date == DateTime.Now.Date)?.Select(x => x.CarId);
                    detail.Column = basementMap[i, j];
                    detail.InProgressWorkers = worker;
                    detail.NotWorkingCar = new CarNotIncluded
                    {
                        CarCode = _unitOfWork.CarsRepository.FindBy(x => carNotInclude.Contains(x.Id))?.Select(x => x.LicensePlates).ToList()
                    };
                    return detail;
                }
            }
            return detail;
        }

        private ColumnsDetail[,] GetDetailsFromExcel(string diagramAttachmentNew)
        {
            try
            {
                using var workbook = new XLWorkbook(diagramAttachmentNew);
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.Range(worksheet.FirstCell(), worksheet.LastCellUsed());
                ColumnsDetail[,] map = new ColumnsDetail[range.RowCount(), range.ColumnCount()];
                if (range == null) return map;
                for (var i = 1; i <= range.RowCount(); i++)
                {
                    for (var j = 1; j <= range.ColumnCount(); j++)
                    {
                        if (!string.IsNullOrEmpty(worksheet.Cell(i, j).Value.ToString()) && !string.IsNullOrWhiteSpace(worksheet.Cell(i, j).Value.ToString()))
                        {
                            var color = "";
                            if (worksheet.Cell(i, j).Style.Fill.BackgroundColor.ColorType.ToString() == "Theme")
                            {
                                color = HexConverter(workbook.Theme.ResolveThemeColor(worksheet.Cell(i, j).Style.Fill.BackgroundColor.ThemeColor).Color);
                            }
                            else
                            {
                                color = HexConverter(worksheet.Cell(i, j).Style.Fill.BackgroundColor.Color);
                            }
                            var column = new ColumnsDetail
                            {
                                Title = worksheet.Cell(i, j).Value.ToString(),
                                Color = color
                            };
                            map[i - 1, j - 1] = column;
                        }
                        else
                        {
                            string color;
                            if (worksheet.Cell(i, j).Style.Fill.BackgroundColor.ColorType.ToString() != "Theme")
                            {
                                color = HexConverter(worksheet.Cell(i, j).Style.Fill.BackgroundColor.Color);
                            }
                            else
                            {
                                color = HexConverter(workbook.Theme
                                    .ResolveThemeColor(worksheet.Cell(i, j).Style.Fill.BackgroundColor.ThemeColor).Color);
                            }

                            map[i - 1, j - 1] = new ColumnsDetail
                            {
                                Title = string.Empty,
                                Color = color
                            };
                        }
                    }
                }
                return map;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }

        }

        public async Task<IPagedResults<Jobs>> GetJobByPlateAndDateAsync(PlaceDateRequest request)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("PlaceID", request.PlaceID);
                p.Add("Day", request.Day ?? DateTime.Now.Date);
                var lstJobs = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Jobs>("uspJobs_selectByPlaceDate", p);


                var total = lstJobs.Count();
                lstJobs = lstJobs.OrderByDescending(x => x.Id);

                return new PagedResults<Jobs>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<Jobs>> GetJobBySlotIDAsync(long? slotID)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("SlotID", slotID);
                var lstJobs = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Jobs>("uspJobs_selectBySlotID", p);


                var total = lstJobs.Count();
                lstJobs = lstJobs.OrderByDescending(x => x.Id);

                return new PagedResults<Jobs>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<Jobs>> GetJobByColIDAsync(long? colID)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("ColumnID", colID);
                var lstJobs = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Jobs>("uspJobs_selectByColumnID", p);


                var total = lstJobs.Count();
                lstJobs = lstJobs.OrderByDescending(x => x.Id);

                return new PagedResults<Jobs>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<BasementViewModel>> GetAllBasementByPlaceAsync(long? placeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("PlaceID", placeId);
                var lstBasements = await _dapperRepository.QueryMultipleUsingStoreProcAsync<BasementViewModel>("uspBasements_selectByPlace", p);
                var basementsEnumerable = lstBasements.ToList();

                var total = basementsEnumerable.Count;
                lstBasements = basementsEnumerable.OrderByDescending(x => x.Id);

                return new PagedResults<BasementViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstBasements,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<SlotViewModel>> GetSlotByPlaceAndDateAsync(PlaceDateRequest request)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("PlaceID", request.PlaceID);
                p.Add("Day", request.Day ?? DateTime.Now.Date);
                var lstSlots = await _dapperRepository.QueryMultipleUsingStoreProcAsync<SlotViewModel>("uspSlots_selectByPlaceDate", p);
                var slotViewModels = lstSlots.ToList();
                var total = slotViewModels.Count;
                lstSlots = slotViewModels.OrderByDescending(x => x.Id);

                return new PagedResults<SlotViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstSlots,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<SlotViewModel>> GetSlotByPlaceTeamAndDateAsync(PlaceDateTeamRequest request)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("PlaceID", request.PlaceID);
                p.Add("Day", request.Day ?? DateTime.Now.Date);
                var lstSlots = await _dapperRepository.QueryMultipleUsingStoreProcAsync<SlotViewModel>("uspSlots_selectByPlaceDate", p);
                lstSlots = lstSlots.Where(x => x.TeamId == request.TeamID);
                var slotViewModels = lstSlots.ToList();
                var total = slotViewModels.Count;
                lstSlots = slotViewModels.OrderByDescending(x => x.Id);

                return new PagedResults<SlotViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstSlots,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<WorkerViewModel>> GetWorkerByTeamAsync(long? request)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("TeamID", request);
                var lstWorkers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<WorkerViewModel>("uspWorker_selectByTeam", p);

                // handle list file
                var workerViewModels = lstWorkers.ToList();

                var total = workerViewModels.Count;
                lstWorkers = workerViewModels.OrderByDescending(x => x.Id);

                var viewModels = lstWorkers.ToList();
                if (viewModels.Count > 0)
                {
                    var queryWorkerPlace = $"SELECT * FROM WorkerPlace WHERE WorkerId IN ({string.Join(",", viewModels.Select(x => x.Id))})";
                    var workerPlace = _dapperRepository.QueryMultiple<WorkerPlaceViewModel>(queryWorkerPlace);
                    var listWorkerPlace = workerPlace.ToList();
                    viewModels.ForEach(x =>
                    {
                        x.ListWorkerPlace = listWorkerPlace.FindAll(d => d.WorkerId == x.Id);
                    });
                }

                return new PagedResults<WorkerViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstWorkers,
                    TotalRecords = total
                };

            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<PlaceTeamZoneViewModel>> GetAllPlaceAsync(PagingRequest request)
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
                var lstPlaces = await _dapperRepository.QueryMultipleUsingStoreProcAsync<PlaceTeamZoneViewModel>("uspPlaces_selectAllRePort", null);
                var placeViewModels = lstPlaces.ToList();

                var total = placeViewModels.Count;
                lstPlaces = !string.IsNullOrEmpty(request.SortField) ? placeViewModels.OrderBy(request) : placeViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstPlaces = lstPlaces.Paging(request);
                }

                if (null == lstPlaces)
                {
                    return new PagedResults<PlaceTeamZoneViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstPlaces.ToList();
                viewModels.ForEach(x =>
                {
                    x.ListTeamViewModel = GetAllTeamByPlaceAsync(x.Id).Result;
                    x.ListZoneViewModel = GetAllZoneByPlaceAsync(x.Id).Result;
                });

                return new PagedResults<PlaceTeamZoneViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        private async Task<List<TeamViewModelReport>> GetAllTeamByPlaceAsync(long placeID)
        {
            try
            {
                var lstWorkers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<WorkerViewModel>("uspWorkers_selectAllNoKey", null);
                var p = new DynamicParameters();
                p.Add("PlaceId", placeID);
                var lstTeams = await _dapperRepository.QueryMultipleUsingStoreProcAsync<TeamViewModelReport>("uspTeams_selectByPlace", p);
                var teamViewModels = lstTeams.ToList();

                var total = teamViewModels.Count;
                lstTeams = teamViewModels.OrderByDescending(x => x.Id);

                var viewModels = lstTeams.ToList();
                if (viewModels.Count > 0)
                {
                    var listTeam = string.Join(",", viewModels.Select(x => x.Id));

                    var queryTeamWorker = $"SELECT [Id],[WorkerId],[TeamId] FROM TeamWorker WHERE TeamId IN ({listTeam})";
                    var teamWorker = _dapperRepository.QueryMultiple<TeamWorker>(queryTeamWorker).ToList();
                    var listTeamWorker = new List<TeamWorkersReport>();
                    foreach (var item in teamWorker)
                    {
                        var workerUS = lstWorkers.Where(x => x.Id == item.WorkerId).FirstOrDefault();
                        listTeamWorker.Add(new TeamWorkersReport { Id = item.Id, TeamId = item.TeamId, WorkerId = item.WorkerId, WorkerName = workerUS?.FullName, AvatarUrl = workerUS?.PictureUrl });
                    }

                    viewModels.ForEach(x =>
                    {
                        x.ListTeamWorker = listTeamWorker.FindAll(d => d.TeamId == x.Id);
                    });
                }

                return viewModels;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        private async Task<List<ZoneViewModelReport>> GetAllZoneByPlaceAsync(long placeID)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("PlaceId", placeID);
                var lstZone = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ZoneViewModelReport>("uspZones_selectByPlace", p);
                var zoneEnumerable = lstZone.ToList();

                var total = zoneEnumerable.Count;
                lstZone = zoneEnumerable.OrderByDescending(x => x.Id);

                var zoneViewModels = lstZone.ToList();
                if (zoneViewModels.Count > 0)
                {

                    zoneViewModels.ForEach(x =>
                    {
                        var listCarInZone = new List<CarViewReport>();
                        var pc = new DynamicParameters();
                        pc.Add("ZoneID", x.Id);
                        var lstCar = _dapperRepository.QueryMultipleUsingStoreProcAsync<CarViewReport>("uspCar_selectByZone", pc).Result.ToList();
                        if (lstCar.Any())
                        {
                            listCarInZone.AddRange(lstCar.Where(d => d.ZoneId == x.Id));
                        }
                        x.ListCar = listCarInZone;
                    });
                }

                return zoneViewModels;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<CarViewReport>> GetCarByZoneAsync(long? zoneId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("ZoneID", zoneId);
                var lstCar = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CarViewReport>("uspCar_selectByZone", p);
                var carsEnumerable = lstCar.ToList();

                var total = carsEnumerable.Count;
                lstCar = carsEnumerable.OrderByDescending(x => x.Id);
                var carViewModels = lstCar.ToList();
                return new PagedResults<CarViewReport>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = carViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ColumnsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<TeamReportViewModel>> ReportTeamAsync(long teamID, long placeId, DateTime? day)
        {
            try
            {
                day ??= DateTime.Now;
                ///////
                // call sql
                var dynamicParameterPlaceTeam = new DynamicParameters();
                dynamicParameterPlaceTeam.Add(Constants.PlaceId, placeId);
                dynamicParameterPlaceTeam.Add(Constants.Day, day);
                dynamicParameterPlaceTeam.Add(Constants.TeamId, teamID);

                var dynamicParameterPlaceTeamStatus = new DynamicParameters();
                dynamicParameterPlaceTeamStatus.Add(Constants.PlaceId, placeId);
                dynamicParameterPlaceTeamStatus.Add(Constants.Day, day);
                dynamicParameterPlaceTeamStatus.Add(Constants.TeamId, teamID);
                dynamicParameterPlaceTeamStatus.Add(Constants.BookStatus, StatusEnum.Approved.GetDescription());

                var dynamicParameterPlaceTeamJobStatus = new DynamicParameters();
                dynamicParameterPlaceTeamJobStatus.Add(Constants.PlaceId, placeId);
                dynamicParameterPlaceTeamJobStatus.Add(Constants.Day, day);
                dynamicParameterPlaceTeamJobStatus.Add(Constants.TeamId, teamID);
                dynamicParameterPlaceTeamJobStatus.Add(Constants.BookStatus, StatusEnum.Approved.GetDescription());
                dynamicParameterPlaceTeamJobStatus.Add(Constants.StatusIP, JobStatusEnum.IN_PROGRESS.ToString());
                dynamicParameterPlaceTeamJobStatus.Add(Constants.StatusBK, JobStatusEnum.BOOKED.ToString());
                dynamicParameterPlaceTeamJobStatus.Add(Constants.StatusDO, JobStatusEnum.DONE.ToString());
                var dynamicParameterZone = new DynamicParameters();
                ////////
                var queryInfoTeamWorker = $"SELECT DISTINCT dbo.UserProfile.FullName FROM dbo.TeamWorker INNER JOIN dbo.Workers ON dbo.TeamWorker.WorkerId = dbo.Workers.Id INNER JOIN dbo.UserProfile ON dbo.Workers.UserId = dbo.UserProfile.Id WHERE dbo.TeamWorker.TeamId = {teamID} AND dbo.Workers.IsDeleted = 0";
                var listUserWorker = _dapperRepository.QueryMultiple<string>(queryInfoTeamWorker).ToList();

                var queryInfoTeamWorkerRegistered = $"SELECT DISTINCT dbo.UserProfile.FullName FROM dbo.TeamWorker INNER JOIN dbo.Workers ON dbo.TeamWorker.WorkerId = dbo.Workers.Id INNER JOIN dbo.UserProfile ON dbo.Workers.UserId = dbo.UserProfile.Id INNER JOIN dbo.Slots ON dbo.TeamWorker.WorkerId = dbo.Slots.WorkerId WHERE dbo.Slots.IsDeleted = 0 AND dbo.Places.Id = {placeId} AND CONVERT(VARCHAR(10),dbo.Slots.Day, 103) = CONVERT(VARCHAR(10), {day}, 103) AND dbo.TeamWorker.TeamId = {teamID} AND dbo.Workers.IsDeleted = 0";
                var listUserWorkerRegistered = _dapperRepository.QueryMultiple<string>(queryInfoTeamWorker).ToList();
                var listUserWorkerUnregistered = listUserWorker.Where(x => listUserWorkerRegistered.Any(y => y == x));
                ////////

                var listInfoUserUnapproval = await _dapperRepository.QueryMultipleUsingStoreProcAsync<UserReportViewModel>("getUser_Unapproval", dynamicParameterPlaceTeamStatus);
                var listInfoUserApprovalUncheckin = await _dapperRepository.QueryMultipleUsingStoreProcAsync<UserReportViewModel>("getUser_Approval_Uncheckin", dynamicParameterPlaceTeamStatus);
                var listUserCarStatus = await _dapperRepository.QueryMultipleUsingStoreProcAsync<UserCarReportViewModel>("getUser_CarStatus", dynamicParameterPlaceTeamJobStatus);
                ///////
                var queryInfoTeamZone = $"SELECT dbo.TeamZone.ZoneId FROM dbo.TeamZone WHERE dbo.TeamZone.TeamId = {teamID}";
                var listInfoTeamZone = _dapperRepository.QueryMultiple<long>(queryInfoTeamZone);
                var queryInfoZone = $"SELECT * FROM dbo.Zones WHERE dbo.Zones.Id IN ({ string.Join(", ", listInfoTeamZone.Select(x => x))})";
                var listInfoZone = _dapperRepository.QueryMultiple<ZoneViewModelReportDetails>(queryInfoZone).ToList();
                if (listInfoZone.Count > 0)
                {

                    listInfoZone.ForEach(x =>
                    {
                        var listCarInZone = new List<CarViewReport>();
                        var pc = new DynamicParameters();
                        pc.Add("ZoneID", x.Id);
                        var lstCar = _dapperRepository.QueryMultipleUsingStoreProcAsync<CarViewReport>("uspCar_selectByZone", pc).Result.ToList();
                        if (lstCar.Any())
                        {
                            listCarInZone.AddRange(lstCar.Where(d => d.ZoneId == x.Id));
                        }
                        x.ListCar = listCarInZone.Select(x => x.LicensePlates).ToList();
                    });
                }
                var result = new TeamReportViewModel
                {
                    ListUserUnregistered = listUserWorkerUnregistered.ToList(),
                    ListUserUnapproval = listInfoUserUnapproval.ToList(),
                    ListUserUnchecking = listInfoUserApprovalUncheckin.ToList(),
                    ListUserCheckIn = listUserCarStatus.ToList(),
                    ListZoneCar = listInfoZone
                };

                return new PagedResult<TeamReportViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ReportService: " + ex.Message);
                throw;
            }
        }
        public async Task<IPagedResults<CustomerViewModelReporterViewModel>> GetInformationForCustomerReport(string userName)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, userName);
                var customer = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CustomerViewModelReporterViewModel>("uspGetInformationForCustomer", p);

                if (null == customer || customer.Count() == 0)
                {
                    return new PagedResults<CustomerViewModelReporterViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var customerCarViewModels = customer.ToList();
                var customerId = customerCarViewModels.FirstOrDefault()?.Id;

                var queryCar = $"SELECT * FROM Cars WHERE CustomerId = {customerId} AND IsDeleted = 0";
                var car = _dapperRepository.QueryMultiple<CarViewModelReport>(queryCar);
                var listCar = car.ToList();

                foreach (var item in customerCarViewModels)
                {
                    item.ListCarDetails = listCar;
                    listCar.ForEach(x =>
                    {
                        x.BrandName = _unitOfWork.BrandsRepository.GetAll().FirstOrDefault(y => y.Id == x.BrandId)?.Title;
                        x.CarModelName = _unitOfWork.CarModelsRepository.GetAll().FirstOrDefault(y => y.Id == x.CarModelId)?.Title;
                        x.SubNow = _unitOfWork.SubscriptionsRepository.GetAll().Where(y => y.EndDate >= DateTime.Now)?.OrderByDescending(y => y.StartDate)?.FirstOrDefault();
                        x.JobNow = _unitOfWork.JobsRepository.GetAll().Where(y => y.BookJobDate == DateTime.Now).ToList();
                        x.JobNear = _unitOfWork.JobsRepository.GetAll().Where(y => y.BookJobDate > DateTime.Now).ToList();
                    });

                }

                var result = customerCarViewModels.ToList();
                return new PagedResults<CustomerViewModelReporterViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = result,
                    TotalRecords = result.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CustomersServices: " + ex.Message);
                throw;
            }
        }
    }
}
