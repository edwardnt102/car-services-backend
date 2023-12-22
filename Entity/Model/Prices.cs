using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Prices")]
    public class Prices : BaseEntity
    {
        public decimal? PriceClassA { get; set; } //Giá dịch vụ phân khúc A

        public decimal? PriceClassB { get; set; } //Giá dịch vụ phân khúc B

        public decimal? PriceClassC { get; set; } //Giá dịch vụ phân khúc C

        public decimal? PriceClassD { get; set; } //Giá dịch vụ phân khúc D

        public decimal? PriceClassE { get; set; } //Giá dịch vụ phân khúc E

        public decimal? PriceClassF { get; set; } //Giá dịch vụ phân khúc F

        public decimal? PriceClassM { get; set; } //Giá dịch vụ phân khúc M

        public decimal? PriceClassS { get; set; } //Giá dịch vụ phân khúc S
    }
}
