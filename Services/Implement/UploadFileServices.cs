using Common;
using Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Repository;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class UploadFileServices : IUploadFileServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string _webRootPath = string.Empty;

        public UploadFileServices(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webRootPath = hostingEnvironment.WebRootPath + Constants.Upload;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                if (file == null || file.Length <= 0) return string.Empty;
                if (!Directory.Exists(_webRootPath))
                {
                    Directory.CreateDirectory(_webRootPath);
                }

                var getExtension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                var fileName = Guid.NewGuid() + getExtension;

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), _webRootPath);
                var fullPath = Path.Combine(pathToSave, fileName);
                Log.Fatal("UploadFile" + fullPath);
                await using var streamAa = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(streamAa).ConfigureAwait(false);
                _unitOfWork.CommitTransaction();
                return fileName;
            }
            catch (Exception e)
            {
                Log.Fatal(DateTime.Now + @"Upload File: " + e.Message);
                Log.Fatal("Error" + e.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteFileByPathAsync(string pathUrl)
        {
            try
            {
                _unitOfWork.OpenTransaction();
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), _webRootPath);
                if (File.Exists(Path.Combine(pathToSave, pathUrl)))
                {
                    File.Delete(Path.Combine(pathToSave, pathUrl));
                    Log.Information("Deleted File: ", pathUrl);
                }
                else
                {
                    Log.Information("File not found: ", pathUrl);
                }

                _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                Log.Fatal(@"Detele File: " + e.Message);
                Log.Fatal("Error" + e.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }

        }

        public bool UploadSingleFileAsync(IFormFile file, string fileNameNew, string fileNameOld)
        {
            try
            {

                if (file == null || file.Length <= 0)
                {
                    return false;
                }
                // Path Folder
                if (!Directory.Exists(_webRootPath))
                {
                    Directory.CreateDirectory(_webRootPath);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), _webRootPath);
                var fullPath = Path.Combine(pathToSave, fileNameNew);
                Log.Fatal("File" + fullPath);
                using var streamAa = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(streamAa);

                if (File.Exists(Path.Combine(pathToSave, fileNameOld)))
                {
                    File.Delete(Path.Combine(pathToSave, fileNameOld));
                    Log.Information("Delete File: ", fileNameOld);
                }
                else
                {
                    Log.Information("File not found: ", fileNameOld);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Fatal(@"UploadSingleFile: " + e.Message);
                Log.Fatal("Error" + e.Message);
                throw;
            }
        }

        public bool UploadMultipleFileAsync(List<IFormFile> files, string fileNameNew, string fileNameOld)
        {
            try
            {
                if (files == null || files.Count <= 0) return false;
                // Path Folder
                if (!Directory.Exists(_webRootPath))
                {
                    Directory.CreateDirectory(_webRootPath);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), _webRootPath);
                Log.Fatal("pathToSave" + pathToSave);
                foreach (var file in files)
                {
                    var fileNameArray = fileNameNew.Split(Constants.Semicolon);
                    foreach (var item in fileNameArray)
                    {

                        var fullPath = Path.Combine(pathToSave, item);
                        Log.Fatal("File" + fullPath);
                        using var streamAa = new FileStream(fullPath, FileMode.Create);
                        file.CopyTo(streamAa);
                    }
                }

                var attachmentFileArray = fileNameOld.Split(Constants.Semicolon);
                foreach (var pathUrl in attachmentFileArray)
                {
                    if (File.Exists(Path.Combine(pathToSave, pathUrl)))
                    {
                        File.Delete(Path.Combine(pathToSave, pathUrl));
                        Log.Information("Delete File: ", pathUrl);
                    }
                    else
                    {
                        Log.Information("File not found: ", pathUrl);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Fatal(@"UploadMultipleFile: " + e.Message);
                Log.Fatal(@"UploadMultipleFile: " + e.Message);
                throw;
            }
        }

        public string GetFileNameSingleFileReNameAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0) return string.Empty;
                var getExtension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                var fileName = file.FileName.Replace(getExtension, string.Empty) + Constants.Underlined + MurmurHash.Hash(file.FileName + Guid.NewGuid());
                return fileName + getExtension;
            }
            catch (Exception e)
            {
                Log.Fatal(@"GetFileNameSingleFileReName: " + e.Message);
                throw;
            }
        }

        public string GetFileNameSingleFileOriginalNameAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0) return string.Empty;
                return file.FileName;
            }
            catch (Exception e)
            {
                Log.Fatal(@"GetFileNameSingleFileOriginalName: " + e.Message);
                throw;
            }
        }

        public string GetFileNameMultipleFileReNameAsync(List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count <= 0) return string.Empty;
                var listAttachmentFile = new List<string>();
                foreach (var file in files)
                {
                    var getExtension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                    var fileName = file.FileName.Replace(getExtension, string.Empty) + Constants.Underlined + MurmurHash.Hash(file.FileName + Guid.NewGuid());
                    listAttachmentFile.Add(fileName + getExtension);
                }
                return string.Join(Constants.Semicolon, listAttachmentFile);
            }
            catch (Exception e)
            {
                Log.Fatal(@"GetFileNameMultipleFileReName: " + e.Message);
                throw;
            }
        }

        public string GetFileNameMultipleFileOriginalNameAsync(List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count <= 0) return string.Empty;
                var listAttachmentFile = new List<string>();
                foreach (var file in files)
                {
                    listAttachmentFile.Add(file.FileName);
                }
                return string.Join(Constants.Semicolon, listAttachmentFile);
            }
            catch (Exception e)
            {
                Log.Fatal(@"GetFileNameMultipleFileOriginalName: " + e.Message);
                throw;
            }
        }
    }
}
