using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("ZoneColumn")]
    public class ZoneColumn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ZoneId { get; set; }

        public long ColumnId { get; set; }
    }
}
