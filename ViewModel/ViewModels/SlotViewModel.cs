using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class SlotViewModel : Slots
    {
        public string RuleName { get; set; }
        public string WorkerName { get; set; }
        public string TeamName { get; set; }
        public string PlaceName { get; set; }
        public string DayPayroll { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
