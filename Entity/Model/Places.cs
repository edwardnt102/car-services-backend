using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Places")]
    public class Places : BaseEntity
    {
        public long? ProvinceId { get; set; } //Thành phố

        public long? DistrictId { get; set; } //Quận

        public long? WardId { get; set; } //Phường

        public long? RuleId { get; set; } //Quy định-Rule mặc định

        public long? PriceId { get; set; } //Quy định-Giá mặc định

        [StringLength(256)]
        public string Address { get; set; } //Địa chỉ

        [StringLength(256)]
        public string Longitude { get; set; } //Kinh độ

        [StringLength(256)]
        public string Latitude { get; set; } //Vĩ độ
    }
}
