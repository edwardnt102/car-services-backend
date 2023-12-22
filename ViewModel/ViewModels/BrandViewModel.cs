using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class BrandViewModel : Brands
    {
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
