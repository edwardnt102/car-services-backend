using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class SubscriptionViewModel : Subscriptions
    {
        public string LicensePlates { get; set; }
        public string ArgentName { get; set; }
        public string PriceName { get; set; }
        public string ClassName { get; set; }
        public string PlaceName { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
