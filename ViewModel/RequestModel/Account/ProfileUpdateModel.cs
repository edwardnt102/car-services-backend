using Microsoft.AspNetCore.Http;
using System;

namespace ViewModel.RequestModel.Account
{
    public class ProfileUpdateModel
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? StreetId { get; set; }
        public long? WardId { get; set; }
        public string PictureUrl { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string GoogleId { get; set; }
    }

    public class AvatarUpdateModel
    {
        public IFormFile Files { get; set; }
    }
}
