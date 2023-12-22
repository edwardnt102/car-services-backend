using Common;
using Common.Constants;
using Common.Pagging;
using Common.Shared;
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
    public class CarsServices : ICarsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public CarsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository,
            IUploadFileServices uploadFileServices, IOptions<AppSettings> options, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _appSettings = options.Value;
            _authenticatedUser = authenticatedUser;
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
                var car = await _unitOfWork.CarsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (car != null && car.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", car.Id);
                    p.Add("@Title", car.Title);
                    p.Add("@TableName", "Cars");
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
                    car.IsDeleted = true;
                    _unitOfWork.CarsRepository.Update(car);
                    await _unitOfWork.CarsRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<CarViewModel>> GetAllAsync(PagingRequest request)
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
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstCars = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CarViewModel>("uspCars_selectAll", p);
                var carViewModels = lstCars.ToList();

                var total = carViewModels.Count;
                lstCars = !string.IsNullOrEmpty(request.SortField) ? carViewModels.OrderBy(request) : carViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstCars = lstCars.Paging(request);
                }

                if (null == lstCars)
                {
                    return new PagedResults<CarViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstCars.ToList();
                viewModels.ForEach(x =>
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

                    var listCarImage = new List<string>();
                    if (!string.IsNullOrEmpty(x.CarImage))
                    {
                        var list = x.CarImage.Split(Constants.Semicolon);
                        listCarImage.AddRange(list.Select(carImage => _appSettings.DomainFile + carImage));
                    }
                    x.ListCarImage = listCarImage;
                });

                return new PagedResults<CarViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstCars = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspCars_Suggestion", p);

                if (null == lstCars)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstCars.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(Cars entity, List<IFormFile> files, List<IFormFile> carImage)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var car = await _unitOfWork.CarsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                var carImageReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(carImage);
                if (car != null && car.Id > 0)
                {
                    var duplicate = _unitOfWork.CarsRepository.FindBy(x => x.Id != entity.Id && x.LicensePlates == entity.LicensePlates && !x.IsDeleted);
                    if (duplicate != null && duplicate.Any())
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = Constants.ErrorMessageCodes.LicensePlatesDuplicate,
                            Data = true
                        };
                    }

                    var fileNameOld = car.AttachmentFileReName ?? string.Empty;
                    var carImageOld = car.CarImage ?? string.Empty;
                    car.History = entity.History;
                    car.Chat = entity.Chat;
                    car.Title = entity.Title;
                    car.Subtitle = entity.Subtitle;
                    car.Description = entity.Description;
                    car.AttachmentFileReName = attachReName;
                    car.AttachmentFileOriginalName = attachOriginalName;
                    car.CustomerId = entity.CustomerId;
                    car.BrandId = entity.BrandId;
                    car.CarModelId = entity.CarModelId;
                    car.YearOfManufacture = entity.YearOfManufacture;
                    car.CarColor = entity.CarColor;
                    car.LicensePlates = entity.LicensePlates;
                    car.CarImage = carImageReName;
                    _unitOfWork.CarsRepository.Update(car);
                    var update = await _unitOfWork.CarsRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                        _uploadFileServices.UploadMultipleFileAsync(carImage, carImageReName, carImageOld);
                    }
                }
                else
                {
                    var duplicate = _unitOfWork.CarsRepository.FindBy(x => x.LicensePlates == entity.LicensePlates && !x.IsDeleted);
                    if (duplicate != null && duplicate.Any())
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = Constants.ErrorMessageCodes.LicensePlatesDuplicate,
                            Data = true
                        };
                    }
                    var model = new Cars
                    {
                        History = entity.History,
                        Chat = entity.Chat,
                        Title = entity.Title,
                        Subtitle = entity.Subtitle,
                        Description = entity.Description,
                        AttachmentFileReName = attachReName,
                        AttachmentFileOriginalName = attachOriginalName,
                        CustomerId = entity.CustomerId,
                        BrandId = entity.BrandId,
                        CarModelId = entity.CarModelId,
                        YearOfManufacture = entity.YearOfManufacture,
                        CarColor = entity.CarColor,
                        LicensePlates = entity.LicensePlates,
                        CarImage = carImageReName,
                    };
                    _unitOfWork.CarsRepository.Add(model);
                    var insert = await _unitOfWork.CarsRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
                        _uploadFileServices.UploadMultipleFileAsync(carImage, carImageReName, string.Empty);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " carsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionCarModelByCarBrandAsync(string searchText, long brandId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.BrandId, brandId);
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);

                var listColumn = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspCars_SuggestionByCarBrand", p);

                if (null == listColumn)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = listColumn.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " carsServices: " + ex.Message);
                throw;
            }
        }
    }
}
