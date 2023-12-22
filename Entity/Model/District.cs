using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("District")]
    public class District
    {
        [Key]
        public long Id { get; set; }

        public long DistrictCode { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        public long? ProvinceId { get; set; }

        public string Prefix { get; set; }
    }
}
