using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class TeamViewModel : Teams
    {
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public List<TeamPlaces> ListTeamPlaces { get; set; }
        public List<TeamWorker> ListTeamWorker { get; set; }
        public List<TeamLead> ListTeamLead { get; set; }
        public List<TeamZone> ListTeamZone { get; set; }
        public string Places { get; set; }
        public string Workers { get; set; }
        public string Leads { get; set; }
        public string Zones { get; set; }
        public string CompanyName { get; set; }
        public string ColorCodeName { get; set; }
    }
    public class TeamViewModelReport : Teams
    {
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public List<TeamWorkersReport> ListTeamWorker { get; set; }
    }
    public class TeamWorkersReport : TeamWorker
    {
        public string WorkerName { get; set; }
        public string AvatarUrl { get; set; }

    }
}
