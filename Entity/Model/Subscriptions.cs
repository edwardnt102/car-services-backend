using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Subscriptions")]
    public class Subscriptions : BaseEntity
    {
        public long? CarId { get; set; } //Chiếc xe-car
        public long? CustomerId { get; set; } //chủ xe
        public long? ArgentId { get; set; } //Đại lý bán-customer
        public long? ClassId { get; set; } //Phân hạng xe
        public long? PlaceId { get; set; } //Khu nhà

        public long? PriceId { get; set; } //Theo bảng giá-Pricelist

        public long? NumberOfMonthsOfPurchase { get; set; } //Số tháng mua
        public decimal? Amount { get; set; } //Số tiền

        [Column(TypeName = "datetime")]
        public DateTime? DateOfPayment { get; set; } //Ngày Thanh toán

        [StringLength(256)]
        public string Promotion { get; set; } //Khuyến mãi

        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; } //Ngày Bat dau

        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; } //Ngày Ket thuc

        public string Status { get; set; } // kiem tra trang thai cua thue bao
    }
}
