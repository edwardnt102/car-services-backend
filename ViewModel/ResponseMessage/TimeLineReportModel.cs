using System;
using System.Collections.Generic;

namespace ViewModel.ResponseMessage
{
    public class TimeLineReportModel
    {
        public string WorkerName { get; set; }
        public int CarsBooked { get; set; }
        public IEnumerable<JobInformation> Jobs { get; set; }
    }

    public class JobTimlineDapper
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string BookJobDate { get; set; }
        public string JobStatus { get; set; }
        public string LicensePlates { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class JobInformation
    {
        public string LicensePlates { get; set; }
        public string JobStatus { get; set; }
        public string BookJobDate { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
