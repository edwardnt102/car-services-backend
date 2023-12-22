using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class ColumnViewModel : Columns
    {
        public string BasementName { get; set; }
        public string PlaceName { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
