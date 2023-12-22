using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Columns")]
    public class Columns : BaseEntity
    {
        public long? BasementId { get; set; } //Tầng hầm-basement
    }
}
