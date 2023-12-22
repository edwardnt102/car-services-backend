using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUploadFileServices
    {
        bool UploadSingleFileAsync(IFormFile file, string fileNameNew, string fileNameOld);
        bool UploadMultipleFileAsync(List<IFormFile> files, string fileNameNew, string fileNameOld);
        Task<string> UploadFileAsync(IFormFile file);
        bool DeleteFileByPathAsync(string pathUrl);
        string GetFileNameSingleFileReNameAsync(IFormFile file);
        string GetFileNameSingleFileOriginalNameAsync(IFormFile file);
        string GetFileNameMultipleFileReNameAsync(List<IFormFile> files);
        string GetFileNameMultipleFileOriginalNameAsync(List<IFormFile> files);
    }
}
