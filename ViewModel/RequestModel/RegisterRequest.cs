using System;

namespace ViewModel.RequestModel
{
    public class RegisterRequest
    {
        // add
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? StreetId { get; set; }

        // update
        public string UserId { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string PhoneNumberOther { get; set; }
    }
}
