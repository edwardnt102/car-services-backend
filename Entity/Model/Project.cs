using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public long Id { get; set; }
        public long ProjectCode { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        public long? ProvinceId { get; set; }

        public long? DistrictId { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
    }
}
