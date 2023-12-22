using Common.Pagging;
using System;

namespace ViewModel.RequestModel.Jobs
{
    public class GetJobREquestModel: PagingRequest
    {
        public string  PlaceName { get; set; }
        public string Phone { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string LicensePlates { get; set; }
    }
}
