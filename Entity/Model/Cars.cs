using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Cars")]
    public class Cars : BaseEntity
    {
        public long? CustomerId { get; set; } //Chủ xe-customer

        public long? BrandId { get; set; } //Hãng xe-Brand

        public long? CarModelId { get; set; } //Mẫu xe-Model

        public long? YearOfManufacture { get; set; } //Năm SX

        [StringLength(15)]
        public string CarColor { get; set; } //Màu xe

        [StringLength(25)]
        public string LicensePlates { get; set; } //Biển số xe

        public string CarImage { get; set; } //Hình ảnh
    }
}
