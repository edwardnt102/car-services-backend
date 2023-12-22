using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("CarModels")]
    public class CarModels : BaseEntity
    {
        public long? BrandId { get; set; } //Hãng-Brand

        public long? ClassId { get; set; } //Phân hạng-Class

        [StringLength(256)]
        public string Note { get; set; } //Lý do phân hạng

        [StringLength(10)]
        public string Long { get; set; } //Dài

        [StringLength(10)]
        public string Width { get; set; } //Rộng

        [StringLength(10)]
        public string High { get; set; } //Cao

        [StringLength(10)]
        public string Heavy { get; set; } //Nặng

        [StringLength(13)]
        public string ReferencePrice { get; set; } //Giá tham khảo

        public string ModelImage { get; set; } //ảnh mẫu

    }
}
