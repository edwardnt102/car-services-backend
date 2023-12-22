using System;

namespace ViewModel.Report
{
    public class SlotReport
    {
        public long Id { get; set; }
        public string SlotName { get; set; }
        public long? WorkerId { get; set; }
        public long? TeamId { get; set; }
        public string BookStatus { get; set; }
        public DateTime? CheckInTime { get; set; }
        public int? NumberOfRegisteredVehicles { get; set; }
    }
}
