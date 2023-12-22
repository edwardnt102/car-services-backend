using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class B2BViewModel : B2B
    {
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public string CompanySharedName { get; set; }
        public string CompanyName { get; set; }
    }
}
