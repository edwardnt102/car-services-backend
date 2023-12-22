using System;

namespace ViewModel.RequestModel.Jobs
{
    public class GetJobBySlotModel
    {
        //j.Id, c.LicensePlates, j.BookJobDate, j.Title
        public long Id { get; set; }
        public string LicensePlates { get; set; }
        public string Title { get; set; }
        public DateTime BookJobDate { get; set; }
        public string JobStatus { get; set; }
        public string Zone { get; set; }
        public string Column { get; set; }
        public string Team { get; set; }
    }
}
