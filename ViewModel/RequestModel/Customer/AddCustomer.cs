using System;

namespace ViewModel.RequestModel.Customer
{
    public class AddCustomer
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; }
        public long? PlaceId { get; set; }
        public long? CustomerId { get; set; }
        public bool IsAgency { get; set; }
        public string RoomNumber { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public string GoogleId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
