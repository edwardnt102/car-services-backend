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
    public class WorkersServices : IWorkersServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;


        public WorkersServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IOptions<AppSettings> options, UserManager<AppUser> userManager, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _appSettings = options.Value;
            _userManager = userManager;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IPagedResults<WorkerViewModel>> GetAllAsync(PagingRequest request)
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
                var lstWorkers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<WorkerViewModel>("uspWorkers_selectAll", p);
                var workerViewModels = lstWorkers.ToList();

                var total = workerViewModels.Count;
                lstWorkers = !string.IsNullOrEmpty(request.SortField) ? workerViewModels.OrderBy(request) : workerViewModels.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstWorkers = lstWorkers.Paging(request);
                }

                if (null == lstWorkers)
                {
                    return new PagedResults<WorkerViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = lstWorkers.ToList();
                if (viewModels.Count > 0)
                {
                    var queryWorkerPlace = $"SELECT wp.[Id], wp.[WorkerId], wp.[PlaceId], CONCAT('Khu nhà: ',p.Title, ' - ', p1.[Name],', ',CONCAT(d.[Prefix], '', d.[Name]),', ',CONCAT(w.[Prefix], '', w.[Name]),CONCAT(s.[Prefix], '', s.[Name]),pr.[Name]) AS LocationName FROM WorkerPlace AS wp LEFT JOIN Places AS p ON p.Id = wp.PlaceId AND p.IsDeleted = 0 LEFT JOIN Province AS p1 ON p1.Id = p.ProvinceId LEFT JOIN District AS d ON d.Id = p.DistrictId LEFT JOIN Ward AS w ON w.WardCode = p.WardId and p.ProvinceId = w.ProvinceId and p.DistrictId = w.DistrictId LEFT JOIN Street AS s ON s.StreetCode = p.WardId and p.ProvinceId = s.ProvinceId and p.DistrictId = s.DistrictId LEFT JOIN Project AS pr ON pr.ProjectCode = p.WardId and p.ProvinceId = pr.ProvinceId and p.DistrictId = pr.DistrictId WHERE wp.WorkerId IN ({string.Join(",", viewModels.Select(x => x.Id))})";
                    var workerPlace = _dapperRepository.QueryMultiple<WorkerPlaceViewModel>(queryWorkerPlace);
                    var listWorkerPlace = workerPlace.ToList();

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
                        x.ListWorkerPlace = listWorkerPlace.FindAll(d => d.WorkerId == x.Id);
                        x.WorkerTypeName = x.WorkerType == 1 ? WorkerType.INTERN.GetDescription() :
                            x.WorkerType == 2 ? WorkerType.OFFICIAL.GetDescription() :
                            x.WorkerType == 3 ? WorkerType.EXPERT.GetDescription() :
                            x.WorkerType == 4 ? WorkerType.TEAMLEAD.GetDescription() : string.Empty;
                        x.LocationName = !string.IsNullOrEmpty(x.WardName) ? x.WardName : !string.IsNullOrEmpty(x.StreetName) ? x.StreetName : x.ProjectName;
                    });
                }

                return new PagedResults<WorkerViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WorkersServices: " + ex.Message);
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
                var lstWorkers = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspWorkers_Suggestion", p);

                if (null == lstWorkers)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstWorkers.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WorkersServices: " + ex.Message);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WorkersServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<decimal>> CheckWalletAsync()
        {
            try
            {

                var work = await _unitOfWork.WorkersRepository.GetSingleAsync(x => x.UserId.ToLower() == _authenticatedUser.UserId.ToLower() && !x.IsDeleted);
                if (work == null)
                {
                    return new PagedResult<decimal>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.WorkerNotFound,
                        Data = 0
                    };
                }
                var slotWorked = _unitOfWork.SlotsRepository.FindBy(x => x.WorkerId == work.Id)?.Select(x => x.TotalAmount);
                if (slotWorked == null)
                {
                    return new PagedResult<decimal>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.SlotsNotFoundMessage,
                        Data = 0
                    };
                }
                var totalWallet = slotWorked.Sum() ?? 0;
                work.MoneyInWallet = totalWallet;
                _unitOfWork.WorkersRepository.Update(work);
                await _unitOfWork.WorkersRepository.CommitAsync();
                return new PagedResult<decimal>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = totalWallet
                };

            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WorkersServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public IPagedResults<ItemModel> SuggestionWorkerTypeAsync()
        {
            try
            {
                var listWorkerType = Enum.GetValues(typeof(WorkerType)).Cast<WorkerType>().Select(x => new ItemModel
                {
                    Id = Convert.ToInt64(x),
                    Value = x.GetDescription().ToString()
                }).ToList();

                var itemModels = listWorkerType.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WorkersServices: " + ex.Message);
                throw;
            }
        }
    }
}
