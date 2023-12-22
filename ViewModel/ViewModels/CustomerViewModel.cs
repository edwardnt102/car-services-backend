using Entity.Model;
using System;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class CustomerViewModel : Customers
    {
        public string PlaceName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string GoogleId { get; set; }
        public string PictureUrl { get; set; }
        public string CustomerName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public string StreetName { get; set; }
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public bool Active { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
    }
}
