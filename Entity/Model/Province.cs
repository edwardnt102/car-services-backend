using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public long Id { get; set; }

        public long ProvinceCode { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Code { get; set; }
    }
}
