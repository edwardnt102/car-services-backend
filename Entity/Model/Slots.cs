using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Slots")]
    public class Slots : BaseEntity
    {
        [Column(TypeName = "datetime")]
        public DateTime? Day { get; set; } //DAY

        public long? PlaceId { get; set; } //Khu nhà-Place

        public long? TeamId { get; set; } //đội team

        public long? WorkerId { get; set; } //lao-dong-worke

        public long? RuleId { get; set; } //quy định RULE

        public bool AutomaticallyGetWorkBeforeDate { get; set; } //tự động nhận việc trước ngày

        public bool AutomaticallyAcceptWorkWithinTheDay { get; set; } //tự động nhận việc trong ngày

        [StringLength(15)]
        public string BookStatus { get; set; } //Tình trạng

        [StringLength(256)]
        public string ReasonCancel { get; set; } //Lý do hủy

        [Column(TypeName = "datetime")]
        public DateTime? TimeToCome { get; set; } //Giờ đến

        [Column(TypeName = "datetime")]
        public DateTime? TimeToGoHome { get; set; } //Giờ về

        [Column(TypeName = "datetime")]
        public DateTime? CheckInTime { get; set; } //Giờ điểm danh đến

        [Column(TypeName = "datetime")]
        public DateTime? CheckOutTime { get; set; } //Giờ điểm danh về

        public string CheckInImage { get; set; } //Ảnh check in

        public string CheckOutImage { get; set; } //Ảnh check out

        public int? NumberOfRegisteredVehicles { get; set; } //Số lượng xe đăng ký

        public int? NumberOfVehiclesReRegistered { get; set; } //Số lượng xe đăng ký lại

        public int? NumberOfBonuses { get; set; } //Số lượng tính thưởng

        [StringLength(256)]
        public string SuppliedMaterials { get; set; } //Vật liệu cung cấp

        [StringLength(256)]
        public string SuppliesReturned { get; set; } //Vật liệu trả lại

        [StringLength(256)]
        public string ChemicalLevel { get; set; } //Hóa chất cung cấp

        [StringLength(256)]
        public string ChemicalReturns { get; set; } //Hóa chất trả lại

        public decimal? UnexpectedBonus { get; set; } //Thưởng đột xuất

        public decimal? UnexpectedPenalty { get; set; } //Phạt đột xuất

        public decimal? TotalAmount { get; set; } //Tổng số tiền

        public decimal? AmountTransferred { get; set; } //Số Tiền đã chuyển về tài khoản

        public decimal? PileRegistration { get; set; } //Tiền cọc

        [Column(TypeName = "datetime")]
        public DateTime? IncomeDeadline { get; set; } // Hạn giải tỏa thu nhập

        [Column(TypeName = "datetime")]
        public DateTime? MoneyTransferDate { get; set; } //Ngày giờ chuyển tiền về tài khoản

    }
}
