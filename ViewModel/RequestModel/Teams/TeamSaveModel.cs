namespace ViewModel.RequestModel.Teams
{
    public class TeamSaveModel : Entity.Model.Teams
    {
        public string ListTeamPlaces { get; set; }
        public string ListTeamWorker { get; set; }
        public string ListTeamLead { get; set; }
        public string ListTeamZone { get; set; }
    }
}
