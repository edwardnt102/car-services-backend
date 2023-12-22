using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Staffs")]
    public class Staffs : BaseEntity
    {
        [StringLength(200)]
        public string UserId { get; set; }

        [StringLength(15)]
        public string IdentificationNumber { get; set; } //Số Căn cước

        [Column(TypeName = "datetime")]
        public DateTime? DateRange { get; set; } //Ngày cấp

        [StringLength(256)]
        public string ProvincialLevel { get; set; } //Tỉnh cấp

        [StringLength(256)]
        public string CurrentJob { get; set; } //Công việc hiện tại

        [StringLength(256)]
        public string CurrentAgency { get; set; } //Cơ quan hiện tại

        [StringLength(256)]
        public string CurrentAccommodation { get; set; } //Chỗ ở hiện tại

        public string PictureOriginalName { get; set; } //Tên ảnh nguyên thủy

        public long? ProvinceId { get; set; } //Thành phố

        public long? DistrictId { get; set; } //Quận

        public long? WardId { get; set; } //Phường
    }
}
