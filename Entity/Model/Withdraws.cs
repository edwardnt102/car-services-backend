using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Withdraws")]
    public class Withdraws : BaseEntity
    {
        [StringLength(13)]
        public string AmountOfWithdrawal { get; set; }

        [StringLength(13)]
        public string AmountBeforeWithdrawal { get; set; }

        [StringLength(13)]
        public string AmountAfterWithdrawal { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TimeToWithdraw { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ReceivingTime { get; set; }

        public long? WorkerId { get; set; }

        public long? StaffId { get; set; }
    }
}
