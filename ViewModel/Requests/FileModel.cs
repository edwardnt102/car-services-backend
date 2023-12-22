using Microsoft.AspNetCore.Http;

namespace ViewModel.Requests
{
    public class FileModel
    {
        public IFormFile File { get; set; }
        public string TableName { get; set; }
    }
}
