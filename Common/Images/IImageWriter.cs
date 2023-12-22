using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Images
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file);
        Task<bool> DeleteImage(string fileUrl);
    }
}

