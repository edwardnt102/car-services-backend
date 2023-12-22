using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ViewModel.RequestModel.Teams
{
    public class TeamsModel : BaseModel
    {
        public List<IFormFile> Attachments { get; set; }
        public List<long> TeamPlaces { get; set; }
        public List<long> TeamZone { get; set; }
    }
}
