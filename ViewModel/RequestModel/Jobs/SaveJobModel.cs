namespace ViewModel.RequestModel.Jobs
{
    public class SaveJobModel : Entity.Model.Jobs
    {
        public long? PlaceId { get; set; }
        public long? BasementId { get; set; }
    }
}
