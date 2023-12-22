using System;

namespace ViewModel.RequestModel.Staff
{
    public class UpdateStaff
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string GoogleId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string IdentificationNumber { get; set; } //Số Căn cước
        public DateTime? DateRange { get; set; } //Ngày cấp
        public string ProvincialLevel { get; set; } //Tỉnh cấp
        public string CurrentJob { get; set; } //Công việc hiện tại
        public string CurrentAgency { get; set; } //Cơ quan hiện tại
        public string CurrentAccommodation { get; set; } //Chỗ ở hiện tại
        public long? ProvinceId { get; set; } //Thành phố
        public long? DistrictId { get; set; } //Quận
        public long? WardId { get; set; } //Phường
        public string ListStaffPlace { get; set; }
    }
}
