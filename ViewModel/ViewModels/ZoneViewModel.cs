using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class ZoneViewModel : Zones
    {
        public string PlaceName { get; set; }
        public string BasementName { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public List<ZoneColumn> ListZoneColumn { get; set; }
        public string ColorCodeName { get; set; }
    }
    public class ZoneViewModelReport : Zones
    {
        public string PlaceName { get; set; }
        public string BasementName { get; set; }
        public List<CarViewReport> ListCar { get; set; }
    }
    public class ZoneViewModelReportDetails : Zones
    {
        public List<string> ListCar { get; set; }
    }
}
