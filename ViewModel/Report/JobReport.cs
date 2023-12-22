using System;

namespace ViewModel.Report
{
    public class JobReport
    {
        public long Id { get; set; }
        public DateTime? BookJobDate { get; set; }
        public long? ColumnId { get; set; }
        public long? SlotInCharge { get; set; }
        public string JobStatus { get; set; }
    }
}
