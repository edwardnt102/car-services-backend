using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    // checked
    [Table("Customers")]
    public class Customers : BaseEntity
    {
        [StringLength(200)]
        public string UserId { get; set; }

        public long? PlaceId { get; set; } //Khu nhà-Place

        [StringLength(150)]
        public string RoomNumber { get; set; } //Số phòng (+Tòa)

        public bool IsAgency { get; set; } //là đại lý

        public long? CustomerId { get; set; } //Đại lý phụ trách-customer

        public string PictureOriginalName { get; set; } //Tên ảnh nguyên thủy
    }
}
