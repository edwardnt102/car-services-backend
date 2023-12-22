using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.RequestModel.Slots
{
    public class BookSlotList
    {
        public long SlotId { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string BookStatus { get; set; }
        public int NumberOfVehicle { get; set; }
        public decimal TotalAmount { get; set; }
    }


}
