using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.RequestModel.Slots
{
    public class BonusSalary
    {
        public decimal? MoldBonus { get; set; }
        public decimal? SignUpBonus { get; set; }
        public decimal? CancellationOfSchedulePenalty { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
