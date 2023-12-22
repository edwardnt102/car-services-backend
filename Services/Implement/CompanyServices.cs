using Common;
using Common.Constants;
using Common.Pagging;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repository;
using Serilog;
using Services.Interfaces;
using Services.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class CompanyServices : ICompanyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;

        public CompanyServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices, IOptions<AppSettings> options)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _appSettings = options.Value;
        }

        public async Task<IPagedResult<bool>> DeleteAsync(long id)
        {
            if (id == 0)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.IdNotFound,
                    Data = false
                };
            }
            try
            {
                await _unitOfWork.OpenTransaction();
                var company = await _unitOfWork.CompanyRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (company != null && company.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", company.Id);
                    p.Add("@Title", company.Title);
                    p.Add("@TableName", "Company");
                    var checkExists = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("spCheckExistsWhenDelete", p);
                    if (checkExists?.FirstOrDefault()?.Value != string.Empty)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = checkExists?.FirstOrDefault()?.Value,
                            Data = false
                        };
                    }
                    company.IsDeleted = true;
                    _unitOfWork.CompanyRepository.Update(company);
                    await _unitOfWork.CompanyRepository.CommitAsync().ConfigureAwait(false);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.DeleteSuccess,
                        Data = true
                    };
                }
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CompanyServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<CompanyViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                request ??= new PagingRequest
                {
                    Page = 1,
                    PageSize = 10,
                    SortDir = Constants.SortDesc,
                    SortField = Constants.SortId
                };
                var p = new DynamicParameters();
                p.Add(Constants.Key, request.SearchText);
                var lstCompany = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CompanyViewModel>("uspCompany_selectAll", p);
                var companyEnumerable = lstCompany.ToList();

                var total = companyEnumerable.Count;
                lstCompany = !string.IsNullOrEmpty(request.SortField) ? companyEnumerable.OrderBy(request) : companyEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstCompany = lstCompany.Paging(request);
                }

                if (null == lstCompany)
                {
                    return new PagedResults<CompanyViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var companyViewModels = lstCompany.ToList();
                companyViewModels.ForEach(x =>
                {
                    var listAttachmentReName = new List<string>();
                    if (!string.IsNullOrEmpty(x.AttachmentFileReName))
                    {
                        var list = x.AttachmentFileReName.Split(Constants.Semicolon);
                        listAttachmentReName.AddRange(list.Select(attachmentFileReName => _appSettings.DomainFile + attachmentFileReName));
                    }
                    x.ListAttachmentReName = listAttachmentReName;

                    var listAttachmentOriginalName = new List<string>();
                    if (!string.IsNullOrEmpty(x.AttachmentFileOriginalName))
                    {
                        var list = x.AttachmentFileOriginalName.Split(Constants.Semicolon);
                        listAttachmentOriginalName.AddRange(list.Select(attachmentFileOriginalName => attachmentFileOriginalName));
                    }
                    x.ListAttachmentOriginalName = listAttachmentOriginalName;
                    x.Logo = !string.IsNullOrEmpty(x.Logo) ? _appSettings.DomainFile + x.Logo : x.Logo;
                    x.Banner = !string.IsNullOrEmpty(x.Banner) ? _appSettings.DomainFile + x.Banner : x.Banner;
                });

                return new PagedResults<CompanyViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = companyViewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CompanyServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(Company entity, List<IFormFile> files, IFormFile logo, IFormFile banner)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var company = await _unitOfWork.CompanyRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);

                var logoReName = _uploadFileServices.GetFileNameSingleFileReNameAsync(logo);
                var bannerReName = _uploadFileServices.GetFileNameSingleFileReNameAsync(banner);
                if (company != null && company.Id > 0)
                {
                    var fileNameOld = company.AttachmentFileReName ?? string.Empty;
                    var logoOld = company.Logo ?? string.Empty;
                    var bannerOld = company.Banner ?? string.Empty;
                    company.Title = entity.Title;
                    company.Subtitle = entity.Subtitle;
                    company.Description = entity.Description;
                    company.AttachmentFileReName = attachReName;
                    company.AttachmentFileOriginalName = attachOriginalName;
                    company.History = entity.History;
                    company.Chat = entity.Chat;
                    company.Logo = logoReName;
                    company.Color = entity.Color;
                    company.Banner = bannerReName;
                    _unitOfWork.CompanyRepository.Update(company);
                    var update = await _unitOfWork.CompanyRepository.CommitAsync().ConfigureAwait(false);
                    Log.Fatal("CompanyRepository");
                    if (update)
                    {
                        Log.Fatal("UploadMultipleFile - UpLoad");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                        _uploadFileServices.UploadSingleFileAsync(logo, logoReName, logoOld);
                        _uploadFileServices.UploadSingleFileAsync(banner, bannerReName, bannerOld);
                    }
                }
                else
                {
                    var model = new Company
                    {
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        History = entity.History,
                        Chat = entity.Chat,
                        Logo = logoReName,
                        Color = entity.Color,
                        Banner = bannerReName
                    };
                    _unitOfWork.CompanyRepository.Add(model);
                    Log.Fatal("CompanyRepository");
                    var insert = await _unitOfWork.CompanyRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        Log.Fatal("UploadMultipleFile New");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
                        _uploadFileServices.UploadSingleFileAsync(logo, logoReName, string.Empty);
                        _uploadFileServices.UploadSingleFileAsync(banner, bannerReName, string.Empty);
                    }
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal("CompanyServices" + ex.Message);
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CompanyServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstCompany = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspCompany_Suggestion", p);

                if (null == lstCompany)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstCompany.ToList();
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = itemModels,
                    TotalRecords = itemModels.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CompanyServices: " + ex.Message);
                throw;
            }
        }
    }
}
