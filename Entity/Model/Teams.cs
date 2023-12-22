using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Teams")]
    public class Teams : BaseEntity
    {
        public long? ColorCodeId { get; set; } //màu
    }
}
