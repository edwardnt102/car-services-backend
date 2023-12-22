using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Jobs")]
    public class Jobs : BaseEntity
    {
        public DateTime? BookJobDate { get; set; } //DAY

        public string PhotoScheduling { get; set; } //Ảnh đặt lịch

        public long? ColumnId { get; set; } //Số cột-clummn

        public long? SubscriptionId { get; set; } //thue-bao-sub

        public long? CarId { get; set; } //Chiếc xe-car

        public long? SlotInCharge { get; set; } //buổi làm-slot phụ trách

        public long? SlotSupport { get; set; } //buổi làm-slot hỗ trợ

        public DateTime? StartingTime { get; set; } //Thời điểm lao động bắt đầu

        public DateTime? EndTime { get; set; } //Thời điểm lao động kết thúc

        public string WorkingStep { get; set; } //Các bước làm việc

        [StringLength(256)]
        public string ChemicalUsed { get; set; } //Hóa chất sử dụng

        [StringLength(256)]
        public string MaterialsUsed { get; set; } //Vật tư sử dụng

        public string MainPhotoBeforeWiping { get; set; } //Ảnh chính trước lau

        public string TheSecondaryPhotoBeforeWiping { get; set; } //Ảnh phụ trước lau

        public string MainPhotoAfterWiping { get; set; } //Ảnh chính sau lau

        public string SubPhotoAfterWiping { get; set; } //Ảnh phụ sau lau

        public long? StaffId { get; set; } //Đội trưởng phụ trách-worker

        [StringLength(256)]
        public string StaffAssessment { get; set; } //Đội trưởng đánh giá

        public int? StaffScore { get; set; } //Điểm đội trưởng đánh giá

        [StringLength(256)]
        public string TeamLeadAssessment { get; set; }

        public int? TeamLeadScore { get; set; }

        [StringLength(256)]
        public string CustomerAssessment { get; set; } //Chủ xe đánh giá

        public int? CustomerScore { get; set; } //Điểm chủ xe đánh giá

        [StringLength(256)]
        public string JobStatus { get; set; } //Trạng thái job

        public long? CustomerId { get; set; } //Chủ xe tại thời điểm đăng ký job
    }
}
