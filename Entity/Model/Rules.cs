using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Rules")]
    public class Rules : BaseEntity
    {
        [Column(TypeName = "datetime")]
        public DateTime? Day { get; set; } //DAY

        public long? PlaceId { get; set; } //Khu nhà-Place

        public decimal? LaborWages { get; set; } //Lương lao động 1 xe

        public decimal? SalarySupervisor { get; set; } //Lương giám sát 1 xe

        public decimal? MinimumQuantity { get; set; } //Số lượng tối thiểu

        public decimal? VehicleSizeFactorA { get; set; } //Hệ số cỡ xe hạng A

        public decimal? VehicleSizeFactorB { get; set; } //Hệ số cỡ xe hạng B

        public decimal? VehicleSizeFactorC { get; set; } //Hệ số cỡ xe hạng C

        public decimal? VehicleSizeFactorD { get; set; } //Hệ số cỡ xe hạng D

        public decimal? VehicleSizeFactorE { get; set; } //Hệ số cỡ xe hạng E

        public decimal? VehicleSizeFactorF { get; set; } //Hệ số cỡ xe hạng F

        public decimal? VehicleSizeFactorM { get; set; } //Hệ số cỡ xe hạng M

        public decimal? VehicleSizeFactorS { get; set; } //Hệ số cỡ xe hạng S

        public decimal? WeatherCoefficient { get; set; } //Hệ số thời tiết

        public decimal? ContingencyCoefficient { get; set; } //Hệ số dự phòng

        public string SignUpBonus { get; set; } //Thưởng đăng ký

        public string MoldBonus { get; set; } //Thưởng mốc

        public string CancellationOfSchedulePenalty { get; set; } //Phạt hủy lịch

        public decimal? PileRegistration { get; set; } //Cọc đăng ký

        public int DayPayroll { get; set; } //Ngày giữ lương
    }
}
