namespace ViewModel.RequestModel.Slots
{
    public class StaffUpdate
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public long? RuleId { get; set; }
        public decimal? UnexpectedBonus { get; set; }
        public decimal? UnexpectedPenalty { get; set; }
    }
}
