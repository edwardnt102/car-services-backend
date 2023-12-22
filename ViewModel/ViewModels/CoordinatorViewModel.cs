using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class CoordinatorViewModel
    {
        public string PlaceBasementName { get; set; }
        public string CurrentDate { get; set; }
        public string RuleName { get; set; }
        public string TotalJobSub { get; set; }
        public string TeamTotal { get; set; }
        public int WorkerUnregisteredTotal { get; set; }
        public string SubscribeCalendar { get; set; }
        public string UnapprovedCarNumber { get; set; }
        public string CarApproved { get; set; }
        public string CarNotAttendance { get; set; }
        public string CarAttendance { get; set; }
        public string JobStatus { get; set; }
        public string ZoneCar { get; set; }
        public string Status { get; set; }
        public List<ListTeamViewModel> ListTeam { get; set; }
        public List<ListZoneViewModel> ListZone { get; set; }
    }
    public class TeamReportViewModel
    {
        public List<string> ListUserUnregistered { get; set; }
        public List<UserReportViewModel> ListUserUnapproval { get; set; }
        public List<UserReportViewModel> ListUserUnchecking { get; set; }
        public List<UserCarReportViewModel> ListUserCheckIn { get; set; }
        public List<ZoneViewModelReportDetails> ListZoneCar { get; set; }
    }
    public class UserReportViewModel
    {
        public string Username { get; set; }
        public int Quantity { get; set; }
    }
    public class UserCarReportViewModel
    {
        public string Username { get; set; }
        public string ListCarIP { get; set; }
        public string ListCarBooked { get; set; }
        public string ListCarDone { get; set; }
    }

}
