using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Basements")]
    public class Basements : BaseEntity
    {
        public long? PlaceId { get; set; } //Khu nhà-Place

        [StringLength(256)]
        public string DiagramAttachmentReName { get; set; } //Sơ đồ cột - lưu file excel

        [StringLength(256)]
        public string DiagramAttachmentOriginalName { get; set; } //Sơ đồ cột - lưu file excel
    }
}
