using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class PlaceViewModel : Places
    {
        public string PriceName { get; set; }
        public string RuleName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public string StreetName { get; set; }
        public string ProjectName { get; set; }
        public string LocationName { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
    public class PlaceTeamZoneViewModel : PlaceViewModel
    {
        public List<TeamViewModelReport> ListTeamViewModel { get; set; }
        public List<ZoneViewModelReport> ListZoneViewModel { get; set; }
    }
}
