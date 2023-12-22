using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Ward")]
    public class Ward
    {
        [Key]
        public long Id { get; set; }

        public long WardCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Prefix { get; set; }

        public long? ProvinceId { get; set; }

        public long? DistrictId { get; set; }
    }
}
