using System.ComponentModel.DataAnnotations;

namespace Entity.Model
{
    public class ColorCode : BaseEntity
    {
        [StringLength(256)]
        public string ColorCodeLevel1 { get; set; } //ColorCodeLevel1

        [StringLength(256)]
        public string ColorCodeLevel2 { get; set; } //ColorCodeLevel2

        [StringLength(256)]
        public string ColorCodeLevel3 { get; set; } //ColorCodeLevel3

        [StringLength(256)]
        public string ColorCodeLevel4 { get; set; } //ColorCodeLevel4

        [StringLength(256)]
        public string ColorCodeLevel5 { get; set; } //ColorCodeLevel5
    }
}
