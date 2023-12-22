using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Workers")]
    public class Workers : BaseEntity
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

        public bool Official { get; set; } //Chính thức

        [Column(TypeName = "decimal(15, 2)")]
        public decimal MoneyInWallet { get; set; } //Số tiền trong ví

        public string PictureOriginalName { get; set; } //Tên ảnh nguyên thủy

        public int WorkerType { get; set; } // kiểu của công nhân (chuyên gia, học việc, chính thức, quản lý)

        public long? ProvinceId { get; set; } //Thành phố

        public long? DistrictId { get; set; } //Quận

        public long? WardId { get; set; } //Phường

        public long? WorkerIntroduceId { get; set; } // Lao dong gioi thieu
    }
}
