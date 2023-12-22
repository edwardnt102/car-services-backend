using System;

namespace ViewModel.RequestModel
{
    public class BookJobRequestModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime BookJobDate { get; set; }
        public string LicensePlate { get; set; }
        public long? SlotSupport { get; set; }
        public long? SlotInCharge { get; set; }
        public long? ColumnId { get; set; }
    }
}
