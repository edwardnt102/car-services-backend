using System.Collections.Generic;
using Entity.Model;

namespace ViewModel.ViewModels
{
    public class RuleViewModel : Rules
    {
        public bool? HangSchedule { get; set; }
        public string NoReplacePenalty { get; set; }
        public float? QuitWorkingPernalty { get; set; }
        public string PlaceName { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
