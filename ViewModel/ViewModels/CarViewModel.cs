using Entity.Model;
using System.Collections.Generic;

namespace ViewModel.ViewModels
{
    public class CarViewModel : Cars
    {
        public string CustomerName { get; set; }
        public string BrandName { get; set; }
        public string CarModelName { get; set; }
        public string CompanyName { get; set; }
        public List<string> ListAttachmentReName { get; set; }
        public List<string> ListAttachmentOriginalName { get; set; }
        public List<string> ListCarImage { get; set; }
    }
    public class CarViewReport
    {
        public long ZoneId { get; set; }
        public long Id { get; set; }
        public string LicensePlates { get; set; }
    }
}
