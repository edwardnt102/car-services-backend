using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Images
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;

        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file);
            return result;
        }

        public async Task<bool> DeleteImage(string file)
        {
            var result = await _imageWriter.DeleteImage(file);
            return result;
        }
    }
}
