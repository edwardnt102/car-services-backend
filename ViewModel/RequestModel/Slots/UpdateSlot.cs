using System;

namespace ViewModel.RequestModel.Slots
{
    public class UpdateSlot
    {
        public long Id { get; set; }
        public DateTime? Day { get; set; }
        public int? NumberOfVehiclesReRegistered { get; set; }
        public int? TeamId { get; set; }
        public int? PlaceId { get; set; }
        public int? UnexpectedBonus  { get; set; }
        public int? UnexpectedPenalty  { get; set; }
        public string SupplyMaterials { get; set; }
        public string SupplyReturned { get; set; }
        public string ChemicalLevel { get; set; }
        public string ChemicalReturn { get; set; }
        public long? RuleId { get; set; }
    }

    public class WorkerUpdateSlot
    {
        //public long Id { get; set; }
        public int? NumberOfVehiclesReRegistered { get; set; }
    }
}
