using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Zones")]
    public class Zones : BaseEntity
    {
        public long? PlaceId { get; set; } //Khu nhà-Place

        public long? BasementId { get; set; } //Tầng hầm-basement

        public string MapOfTunnelsInTheArea { get; set; } //bản đồ

        public long? ColorCodeId { get; set; } //màu
    }
}
