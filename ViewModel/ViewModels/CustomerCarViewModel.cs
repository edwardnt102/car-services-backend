using System.Collections.Generic;
using Entity.Model;

namespace ViewModel.ViewModels
{
    public class CustomerCarViewModel : Customers
    {
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProvinceId { get; set; }
        public string StreetId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string GoogleId { get; set; }
        public string PictureUrl { get; set; }
        public string PhoneNumberOther { get; set; }
        public List<Cars> ListCar { get; set; }
    }
}