using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(256)]
        public string Title { get; set; } //Tiêu đề

        [StringLength(256)]
        public string Subtitle { get; set; } //Phụ đề

        [StringLength(256)]
        public string Description { get; set; } //Sự miêu tả

        public string AttachmentFileReName { get; set; } //Tệp đính kèm

        public string AttachmentFileOriginalName { get; set; } //Tệp đính kèm nguyên thủy

        [StringLength(256)]
        public string History { get; set; } //Lịch sử

        [StringLength(256)]
        public string Chat { get; set; } //Trò chuyện

        [StringLength(50)]
        public string CreatedBy { get; set; } //Được tạo bởi

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; } //Ngày tạo ra

        [StringLength(50)]
        public string ModifiedBy { get; set; } //Được sửa đổi bởi

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; } //Ngày sửa đổi

        public bool IsDeleted { get; set; } //Bị xóa

        public long? CompanyId { get; set; } //Được sửa đổi bởi
    }
}
