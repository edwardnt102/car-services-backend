using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ViewModel.RequestModel.Basements
{
    public class BasementSaveModel : BaseModel
    {
        public IFormFile Diagram { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public long? PlaceId { get; set; }

    }
}
