using System;

namespace ViewModel.RequestModel.Slots
{
    public class CreateSlot
    {
        public string Title { get; set; }
        public int? WorkerId { get; set; }
        public DateTime? Day { get; set; }
        public int? TeamId { get; set; }
        public int? PlaceId { get; set; }
        public int NumberOfRegisteredVehicles { get; set; }
    }

    public class CreateSlotResponse {
        public long WorkerId { get; set; }
        public long PlaceId { get; set; }
        public long TeamId { get; set; }
        public long RuleId { get; set; }
        public decimal PileRegistration { get; set; }
        public int WorkerType { get; set; }
    }
}
