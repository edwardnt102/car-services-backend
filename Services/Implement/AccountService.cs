using Common.Constants;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repository;
using Serilog;
using Services.Interfaces;
using Services.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using ViewModel.RequestModel.Account;
using ViewModel.RequestModel.Customer;
using ViewModel.RequestModel.Staff;
using ViewModel.RequestModel.Worker;

namespace Services.Implement
{
    public class AccountServices : IAccountServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly IDapperRepository _dapperRepository;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly AppSettings _appSettings;

        public AccountServices(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork,
            IUploadFileServices uploadFileServices, IDapperRepository dapperRepository, IAuthenticatedUserService authenticatedUserService, IOptions<AppSettings> options)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _uploadFileServices = uploadFileServices;
            _dapperRepository = dapperRepository;
            _authenticatedUserService = authenticatedUserService;
            _appSettings = options.Value;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            try
            {
                if (string.IsNullOrEmpty(roleName))
                {
                    return false;
                }

                if (!await _roleManager.RoleExistsAsync(roleName).ConfigureAwait(false))
                {
                    var roleNormal = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(roleNormal).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " AccountServices: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateUserRoleAsync(string useName, string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName.Trim()).ConfigureAwait(false);
                var user = await _userManager.FindByNameAsync(useName.Trim()).ConfigureAwait(false);

                if (role == null || user == null)
                {
                    return false;
                }

                if (!await _userManager.IsInRoleAsync(user, role.Name).ConfigureAwait(false))
                {
                    await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " AccountServices: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteUserRoleAsync(string useName, string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName.Trim()).ConfigureAwait(false);
                var user = await _userManager.FindByNameAsync(useName.Trim()).ConfigureAwait(false);

                if (role == null || user == null)
                {
                    return false;
                }

                if (await _userManager.IsInRoleAsync(user, role.Name).ConfigureAwait(false))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name).ConfigureAwait(false);
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + ": " + ex.Message);
                throw;
            }
        }

        public async Task<AppUser> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IPagedResult<AppUser>> AddCustomerAsync(AddCustomer model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var userExists = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false);
                if (userExists != null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                        ResponseMessage = Constants.ErrorMessageCodes.UserAlreadyExists
                    };
                }

                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    FullName = model.FullName,
                    Address = model.Address,
                    Facebook = model.Facebook,
                    Zalo = model.Zalo,
                    GoogleId = model.GoogleId,
                    Active = true,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.UserCreateFail
                    };
                }

                await CreateRoleAsync(Constants.Strings.RoleIdentifiers.Customer).ConfigureAwait(false);
                await CreateUserRoleAsync(model.UserName, Constants.Strings.RoleIdentifiers.Customer).ConfigureAwait(false);
                var customer = new Customers
                {
                    UserId = user.Id,
                    PlaceId = model.PlaceId,
                    CustomerId = model.CustomerId,
                    IsAgency = model.IsAgency,
                    RoomNumber = model.RoomNumber,
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Description = model.Description
                };
                _unitOfWork.CustomersRepository.Add(customer);
                await _unitOfWork.CustomersRepository.CommitAsync().ConfigureAwait(false);

                _unitOfWork.CommitTransaction();
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.UserCreateSuccess
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - AccountServices AddCustomer Exception: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<AppUser>> UpdateCustomerAsync(UpdateCustomer model, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var user = await _userManager.FindByIdAsync(model.UserId.ToLower()).ConfigureAwait(false);
                if (user == null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }

                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Gender = model.Gender;
                user.DateOfBirth = model.DateOfBirth;
                user.FullName = model.FullName;
                user.Facebook = model.Facebook;
                user.Zalo = model.Zalo;
                user.GoogleId = model.GoogleId;
                user.Address = model.Address;


                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.SaveFail
                    };
                }
                var customer = await _unitOfWork.CustomersRepository.GetSingleAsync(x => x.UserId.ToLower() == model.UserId.ToLower() && !x.IsDeleted);
                if (customer != null && customer.Id > 0)
                {
                    var fileNameCustomerOld = customer.AttachmentFileReName;
                    customer.PlaceId = model.PlaceId;
                    customer.CustomerId = model.CustomerId;
                    customer.IsAgency = model.IsAgency;
                    customer.RoomNumber = model.RoomNumber;
                    customer.Title = model.Title;
                    customer.Subtitle = model.Subtitle;
                    customer.Description = model.Description;
                    customer.AttachmentFileReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                    customer.AttachmentFileOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);

                    _unitOfWork.CustomersRepository.Update(customer);
                    var update = await _unitOfWork.CustomersRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, customer.AttachmentFileReName, fileNameCustomerOld);
                    }
                }

                _unitOfWork.CommitTransaction();
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - AccountServices UpdateCustomer Exception: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<AppUser>> AddStaffAsync(AddStaff model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var userExists = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false);
                if (userExists != null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                        ResponseMessage = Constants.ErrorMessageCodes.UserAlreadyExists
                    };
                }

                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    FullName = model.FullName,
                    Facebook = model.Facebook,
                    Zalo = model.Zalo,
                    GoogleId = model.GoogleId,
                    Address = model.Address,
                    Active = true,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.UserCreateFail
                    };
                }

                await CreateRoleAsync(Constants.Strings.RoleIdentifiers.Staff).ConfigureAwait(false);
                await CreateUserRoleAsync(model.UserName, Constants.Strings.RoleIdentifiers.Staff).ConfigureAwait(false);
                var staff = new Staffs()
                {
                    UserId = user.Id,
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Description = model.Description,
                    IdentificationNumber = model.IdentificationNumber,
                    DateRange = model.DateRange,
                    ProvincialLevel = model.ProvincialLevel,
                    CurrentJob = model.CurrentJob,
                    CurrentAgency = model.CurrentAgency,
                    CurrentAccommodation = model.CurrentAccommodation,
                    ProvinceId = model.ProvinceId,
                    DistrictId = model.DistrictId,
                    WardId = model.WardId
                };
                _unitOfWork.StaffsRepository.Add(staff);
                var insert = await _unitOfWork.StaffsRepository.CommitAsync().ConfigureAwait(false);
                if (insert)
                {
                    SaveStaffPlace(staff.Id, model.ListStaffPlace);
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.UserCreateSuccess
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - AccountServices AddStaff Exception: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<AppUser>> UpdateStaffAsync(UpdateStaff model, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var user = await _userManager.FindByIdAsync(model.UserId.ToLower()).ConfigureAwait(false);
                if (user == null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }

                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Gender = model.Gender;
                user.DateOfBirth = model.DateOfBirth;
                user.FullName = model.FullName;
                user.Address = model.Address;
                user.Facebook = model.Facebook;
                user.Zalo = model.Zalo;
                user.GoogleId = model.GoogleId;

                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.SaveFail
                    };
                }
                var staff = await _unitOfWork.StaffsRepository.GetSingleAsync(x => x.UserId.ToLower() == model.UserId.ToLower() && !x.IsDeleted);
                if (staff != null && staff.Id > 0)
                {
                    var fileNameStaffOld = staff.AttachmentFileReName;
                    staff.Title = model.Title;
                    staff.Subtitle = model.Subtitle;
                    staff.Description = model.Description;
                    staff.IdentificationNumber = model.IdentificationNumber;
                    staff.DateRange = model.DateRange;
                    staff.ProvincialLevel = model.ProvincialLevel;
                    staff.CurrentJob = model.CurrentJob;
                    staff.CurrentAgency = model.CurrentAgency;
                    staff.CurrentAccommodation = model.CurrentAccommodation;
                    staff.ProvinceId = model.ProvinceId;
                    staff.DistrictId = model.DistrictId;
                    staff.WardId = model.WardId;
                    staff.AttachmentFileReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                    staff.AttachmentFileOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);

                    _unitOfWork.StaffsRepository.Update(staff);
                    var update = await _unitOfWork.StaffsRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, staff.AttachmentFileReName, fileNameStaffOld);
                        SaveStaffPlace(staff.Id, model.ListStaffPlace);
                    }
                }

                _unitOfWork.CommitTransaction();
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - AccountServices UpdateStaff Exception: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<AppUser>> AddWorkerAsync(AddWorker model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var userExists = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false);
                if (userExists != null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                        ResponseMessage = Constants.ErrorMessageCodes.UserAlreadyExists
                    };
                }

                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    FullName = model.FullName,
                    Facebook = model.Facebook,
                    Zalo = model.Zalo,
                    GoogleId = model.GoogleId,
                    Address = model.Address,
                    Active = true,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.UserCreateFail
                    };
                }

                await CreateRoleAsync(Constants.Strings.RoleIdentifiers.Worker).ConfigureAwait(false);
                await CreateUserRoleAsync(model.UserName, Constants.Strings.RoleIdentifiers.Worker).ConfigureAwait(false);
                var worker = new Workers()
                {
                    UserId = user.Id,
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Description = model.Description,
                    IdentificationNumber = model.IdentificationNumber,
                    DateRange = model.DateRange,
                    ProvincialLevel = model.ProvincialLevel,
                    CurrentJob = model.CurrentJob,
                    CurrentAgency = model.CurrentAgency,
                    CurrentAccommodation = model.CurrentAccommodation,
                    ProvinceId = model.ProvinceId,
                    DistrictId = model.DistrictId,
                    WardId = model.WardId,
                    Official = model.Official,
                    WorkerType = model.WorkerType,
                    WorkerIntroduceId = model.WorkerIntroduceId
                };

                _unitOfWork.WorkersRepository.Add(worker);
                var insert = await _unitOfWork.WorkersRepository.CommitAsync().ConfigureAwait(false);
                if (insert)
                {
                    SaveWorkerPlace(worker.Id, model.ListWorkerPlace);
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.UserCreateSuccess
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - AccountServices AddWorker Exception: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<AppUser>> UpdateWorkerAsync(UpdateWorker model, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var user = await _userManager.FindByIdAsync(model.UserId.ToLower()).ConfigureAwait(false);
                if (user == null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }

                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Gender = model.Gender;
                user.DateOfBirth = model.DateOfBirth;
                user.FullName = model.FullName;
                user.Facebook = model.Facebook;
                user.Zalo = model.Zalo;
                user.GoogleId = model.GoogleId;
                user.Address = model.Address;

                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.ErrorMessageCodes.SaveFail
                    };
                }
                var worker = await _unitOfWork.WorkersRepository.GetSingleAsync(x => x.UserId.ToLower() == model.UserId.ToLower() && !x.IsDeleted);
                if (worker != null && worker.Id > 0)
                {
                    var fileNameWorkerOld = worker.AttachmentFileReName;
                    worker.Title = model.Title;
                    worker.Subtitle = model.Subtitle;
                    worker.Description = model.Description;
                    worker.IdentificationNumber = model.IdentificationNumber;
                    worker.DateRange = model.DateRange;
                    worker.ProvincialLevel = model.ProvincialLevel;
                    worker.CurrentJob = model.CurrentJob;
                    worker.CurrentAgency = model.CurrentAgency;
                    worker.CurrentAccommodation = model.CurrentAccommodation;
                    worker.ProvinceId = model.ProvinceId;
                    worker.DistrictId = model.DistrictId;
                    worker.WardId = model.WardId;
                    worker.Official = model.Official;
                    worker.WorkerType = model.WorkerType;
                    worker.WorkerIntroduceId = model.WorkerIntroduceId;
                    worker.AttachmentFileReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                    worker.AttachmentFileOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);

                    _unitOfWork.WorkersRepository.Update(worker);
                    var update = await _unitOfWork.WorkersRepository.CommitAsync().ConfigureAwait(false);
                    if (update)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, worker.AttachmentFileReName, fileNameWorkerOld);
                        SaveWorkerPlace(worker.Id, model.ListWorkerPlace);
                    }
                }

                _unitOfWork.CommitTransaction();
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - AccountServices UpdateWorker Exception: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private void SaveWorkerPlace(long workerId, string listWorkerPlace)
        {
            // delete when model.ListWorkerPlace
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(Constants.Key, workerId);
            _dapperRepository.InsertOrUpdateUsingStoreProc<WorkerPlace>("uspWorkerPlace_Delete", dynamicParameters);

            var list = JsonConvert.DeserializeObject<List<WorkerPlace>>(listWorkerPlace);

            if (list.Count <= 0) return;
            // insert WorkerPlace
            list.ForEach(x => x.WorkerId = workerId);
            _unitOfWork.WorkerPlaceRepository.BulkInsert(list);
        }

        public async Task<IPagedResult<bool>> UpdateProfileAsync(ProfileUpdateModel request)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(_authenticatedUserService.UserId);
                if (user == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }
                user.FullName = request.FullName;
                user.DateOfBirth = request.DateOfBirth;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;
                user.Gender = request.Gender;
                user.Address = request.Address;
                user.PictureUrl = request.PictureUrl;
                user.Facebook = request.Facebook;
                user.Zalo = request.Zalo;
                user.GoogleId = request.GoogleId;

                await _userManager.UpdateAsync(user);
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.InternalServerErrorMessage
                };
            }
        }

        public async Task<IPagedResult<string>> UpdateAvatarAsync(IFormFile files)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(_authenticatedUserService.UserId);
                if (user == null)
                {
                    return new PagedResult<string>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }
                user.PictureUrl = await _uploadFileServices.UploadFileAsync(files);
                await _userManager.UpdateAsync(user);
                return new PagedResult<string>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    Data = _appSettings.DomainFile + user.PictureUrl
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                return new PagedResult<string>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.InternalServerErrorMessage
                };
            }
        }

        public async Task<IPagedResult<bool>> ChangePassword(ChangePasswordModel request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_authenticatedUserService.UserId);
                if (user == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }
                var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (!result.Succeeded)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.ChangePasswordFail,
                        ResponseMessage = Constants.ErrorMessageCodes.ChangePasswordFailMessage
                    };
                }
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.InternalServerErrorMessage
                };
            }
        }

        public async Task<IPagedResult<AppUser>> GetProfileDetail()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_authenticatedUserService.UserId);

                if (user == null)
                {
                    return new PagedResult<AppUser>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.UserNotFoundMessage
                    };
                }

                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                return new PagedResult<AppUser>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.ErrorMessageCodes.InternalServerErrorMessage
                };
            }
        }

        private void SaveStaffPlace(long staffId, string listStaffPlace)
        {
            // delete when model.ListWorkerPlace
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(Constants.Key, staffId);
            _dapperRepository.InsertOrUpdateUsingStoreProc<StaffPlace>("uspStaffPlace_Delete", dynamicParameters);

            var list = JsonConvert.DeserializeObject<List<StaffPlace>>(listStaffPlace);

            if (list.Count <= 0) return;
            // insert WorkerPlace
            list.ForEach(x => x.StaffId = staffId);
            _unitOfWork.StaffPlaceRepository.BulkInsert(list);
        }
    }
}
