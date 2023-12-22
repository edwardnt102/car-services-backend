using System;

namespace ViewModel.RequestModel.Jobs
{
    public class GetJobResponseModel : Entity.Model.Jobs
    {
        public string LicensePlates { get; set; }
        public string PlaceName { get; set; }
        public string WorkerName { get; set; }
        public DateTime JobBookDate { get; set; }
        public string ColumnName { get; set; }
        public long? PlaceId { get; set; }
        public long? BasementId { get; set; }
        public string Subcription { get; set; }
        public string SlotInChargeTitle { get; set; }
        public string SlotSupportTitle { get; set; }
        public string CompanyName { get; set; }
    }
}
