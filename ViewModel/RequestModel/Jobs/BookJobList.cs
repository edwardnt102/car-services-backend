using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.RequestModel.Jobs
{
    public class BookJobList
    {
        public string LicensePlates { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndTime { get; set; }
        public string JobStatus { get; set; }
    }
}
