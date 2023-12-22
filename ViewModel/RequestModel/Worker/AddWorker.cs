using System;

namespace ViewModel.RequestModel.Worker
{
    public class AddWorker
    {
        public string UserName { get; set; }
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
        public string IdentificationNumber { get; set; }
        public DateTime? DateRange { get; set; }
        public string ProvincialLevel { get; set; }
        public string CurrentJob { get; set; }
        public string CurrentAgency { get; set; }
        public string CurrentAccommodation { get; set; }
        public long? ProvinceId { get; set; } //Thành phố
        public long? DistrictId { get; set; } //Quận
        public long? WardId { get; set; } //Phường
        public bool Official { get; set; } //Chính thức
        public int WorkerType { get; set; }
        public long? WorkerIntroduceId { get; set; } // Lao dong gioi thieu
        public string ListWorkerPlace { get; set; }
    }
}
