using Entity.Model;

namespace ViewModel.RequestModel
{
    public class RuleRequest : Rules
    {
        public bool? HangSchedule { get; set; }
        public string NoReplacePenalty { get; set; }
        public float? QuitWorkingPernalty { get; set; }
    }

    public class RuleRewardAndPunish
    {
        public int Unit { get; set; }
        public float Price { get; set; }
    }
    public class CancellationOfSchedulePenalty
    {
        public CancellationOfSchedulePenalty(bool? hangSche, string noRepPen, float? quitworking)
        {
            this.HangSchedule = hangSche;
            this.NoReplacePenalty = noRepPen;
            this.QuitWorkingPernalty = quitworking;
        }
        public bool? HangSchedule { get; set; }
        public string NoReplacePenalty { get; set; }
        public float? QuitWorkingPernalty { get; set; }
    }
}
