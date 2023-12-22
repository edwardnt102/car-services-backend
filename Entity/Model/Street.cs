using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Street")]
    public class Street
    {
        [Key]
        public long Id { get; set; }

        public long StreetCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Prefix { get; set; }

        public long? ProvinceId { get; set; }

        public long? DistrictId { get; set; }
    }
}
