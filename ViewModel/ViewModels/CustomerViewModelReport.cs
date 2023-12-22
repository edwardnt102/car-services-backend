using Entity.Model;
using System;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class CustomerViewModelReporterViewModel : Customers
    {
        public string PlaceName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberOther { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public long? ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public long? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long? StreetId { get; set; }
        public string StreetName { get; set; }
        public long? WardId { get; set; }
        public string WardName { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string GoogleId { get; set; }
        public string PictureUrl { get; set; }
        public string CustomerName { get; set; }
        public bool Active { get; set; }
        public List<CarViewModelReport> ListCarDetails { get; set; }

    }
    public class CarViewModelReport : Cars
    {
        public string BrandName { get; set; }
        public string CarModelName { get; set; }
        public Subscriptions SubNow { get; set; }
        public List<Jobs> JobNow { get; set; }
        public List<Jobs> JobNear { get; set; }
    }
}
