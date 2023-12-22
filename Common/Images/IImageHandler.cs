using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Images
{
    public interface IImageHandler
    {
        Task<string> UploadImage(IFormFile file);
        Task<bool> DeleteImage(string file);
    }
}
