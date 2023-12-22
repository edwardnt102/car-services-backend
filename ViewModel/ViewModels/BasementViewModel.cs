using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class BasementViewModel : Basements
    {
        public string PlaceName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public string CompanyName { get; set; }
        public string ListColumn { get; set; }
    }

    public class BasementMap {
        public string ColumnName { get; set; }
        public List<ColumnsDetail> Columns { get; set; }
    }



    public class ColumnsDetail
    {
        public string Title { get; set; }
        public string Color { get; set; }
    }

    public class BasementDetail
    {
        public string BasementName { get; set; }
        public long BasementId { get; set; }
        public List<BasementMap>Columns { get; set; }
    }
}
