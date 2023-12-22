namespace ViewModel.ViewModels
{
    public class ListTeamViewModel
    {
        public long TeamId { get; set; }
        public string TeamTotal { get; set; }
        public int WorkerUnregisteredTotal { get; set; }
        public string SubscribeCalendar { get; set; }
        public string UnapprovedCarNumber { get; set; }
        public string CarApproved { get; set; }
        public string CarNotAttendance { get; set; }
        public string CarAttendance { get; set; }
        public string JobStatus { get; set; }
        public string ColorCode { get; set; }
    }
}
