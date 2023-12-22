using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class PriceViewModel : Prices
    {
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
