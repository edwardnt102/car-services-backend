using Common;
using Common.Constants;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Identity;
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
    public class CustomersServices : ICustomersServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthenticatedUserService _userService;

        public CustomersServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IOptions<AppSettings> options, UserManager<AppUser> userManager, IAuthenticatedUserService userService)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _appSettings = options.Value;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IPagedResults<CustomerViewModel>> GetAllAsync(PagingRequest request)
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
                p.Add(Constants.CompanyId, _userService.CompanyId);
                var lstCustomers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CustomerViewModel>("uspCustomers_selectAll", p);
                var customerViewModels = lstCustomers.ToList();

                var total = customerViewModels.Count;
                lstCustomers = !string.IsNullOrEmpty(request.SortField) ? customerViewModels.OrderBy(request) : customerViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstCustomers = lstCustomers.Paging(request);
                }

                if (null == lstCustomers)
                {
                    return new PagedResults<CustomerViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstCustomers.ToList();
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
                    x.PictureUrl = !string.IsNullOrEmpty(x.PictureUrl) ? _appSettings.DomainFile + x.PictureUrl : string.Empty;
                    x.LocationName = !string.IsNullOrEmpty(x.WardName) ? x.WardName : !string.IsNullOrEmpty(x.StreetName) ? x.StreetName : x.ProjectName;
                });

                return new PagedResults<CustomerViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CustomersServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _userService.CompanyId);
                var lstCustomers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspCustomers_Suggestion", p);

                if (null == lstCustomers)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstCustomers.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CustomersServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<CustomerCarViewModel>> GetInformationForCustomerAsync(string userName)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, userName);
                var customer = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CustomerCarViewModel>("uspGetInformationForCustomer", p);

                if (null == customer)
                {
                    return new PagedResults<CustomerCarViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var customerCarViewModels = customer.ToList();
                var customerId = customerCarViewModels.FirstOrDefault()?.Id;

                var queryCar = $"SELECT * FROM Cars WHERE CustomerId = {customerId} AND IsDeleted = 0";
                var car = _dapperRepository.QueryMultiple<Cars>(queryCar);
                var listCar = car.ToList();

                customerCarViewModels.ForEach(x =>
                {
                    x.ListCar = listCar;
                });

                var result = customerCarViewModels.ToList();
                return new PagedResults<CustomerCarViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = result,
                    TotalRecords = result.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CustomersServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InActiveUserAsync(string userId, bool active)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.UserIdNotFound,
                    Data = false
                };
            }
            try
            {
                await _unitOfWork.OpenTransaction();
                var user = await _userManager.FindByIdAsync(userId.ToLower()).ConfigureAwait(false);
                if (user == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage,
                        Data = false
                    };
                }
                user.Active = active;
                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.InActiveUserFail,
                        Data = false
                    };
                }
                _unitOfWork.CommitTransaction();
                if (active)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.ActiveUserSuccess,
                        Data = true
                    };
                }
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.InActiveUserSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CustomersServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
