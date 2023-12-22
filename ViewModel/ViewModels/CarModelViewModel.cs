using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class CarModelViewModel : CarModels
    {
        public string BrandName { get; set; }
        public string ClassName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public List<string> ListModelImage { get; set; }
    }
}
