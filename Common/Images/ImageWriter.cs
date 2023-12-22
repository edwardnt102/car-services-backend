using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Images
{
    public class ImageWriter : IImageWriter
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageWriter(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return "Invalid image file";
        }
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }
        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);
                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }

        public Task<bool> DeleteImage(string fileUrl)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileUrl);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return new Task<bool>(() => true);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                return new Task<bool>(() => false);
            }
        }
    }
}
