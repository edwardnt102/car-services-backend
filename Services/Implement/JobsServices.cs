using AutoMapper;
using Common;
using Common.Constants;
using Common.Extentions;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using Repository;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utility;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel;
using ViewModel.RequestModel.Jobs;

namespace Services.Implement
{
    public class JobsServices : IJobsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IUploadFileServices _uploadFileServices;

        public JobsServices(IUnitOfWork unitOfWork, IMapper mapper, IDapperRepository dapperRepository,
            IModelUtility modelUtility, IAuthenticatedUserService authenticatedUserService,
            IUploadFileServices uploadFileServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
            _authenticatedUserService = authenticatedUserService;
            _uploadFileServices = uploadFileServices;
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
                var job = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (job != null && job.Id > 0)
                {
                    job.IsDeleted = true;
                    _unitOfWork.JobsRepository.Update(job);
                    await _unitOfWork.JobsRepository.CommitAsync().ConfigureAwait(false);
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public IPagedResults<GetJobResponseModel> GetAllAsync(GetJobREquestModel request)
        {
            try
            {
                if (null == request)
                {
                    request = new GetJobREquestModel
                    {
                        Page = 1,
                        PageSize = 10,
                        SortDir = Constants.SortDesc,
                        SortField = Constants.SortId
                    };
                }
                var p = new DynamicParameters();
                if (string.IsNullOrWhiteSpace(request.SearchText))
                {
                    request.SearchText = string.Empty;
                }

                p.Add("@FromDate", request.FromDate.HasValue ? request.FromDate.Value.Date : DateTime.Now.Date);
                p.Add("@LicensePlates", request.LicensePlates ?? string.Empty);
                p.Add("@Phone", request.Phone ?? string.Empty);
                p.Add("@ToDate", request.ToDate.HasValue ? request.ToDate.Value.Date : DateTime.Now.Date);
                p.Add("@PlaceName", request.PlaceName ?? string.Empty);
                p.Add(Constants.CompanyId, _authenticatedUserService.CompanyId);
                var query = @"
                  DECLARE @company NVARCHAR(100)
		            select TOP 1 @company = CompanyShareds from B2B 
		            where CompanyId=@CompanyId and DataType='Jobs';

                  SELECT co.BasementId, car.LicensePlates, j.BookJobDate as JobBookDate, j.JobStatus, u.FullName as WorkerName, p.Title as PlaceName, co.Title as ColumnName, p.Id AS PlaceId, s.Title as Subcription, sl.Title as SlotInChargeTitle, sl1.Title as SlotSupportTitle,j.*,com.Title as CompanyName
                  FROM dbo.Jobs j 
                  left JOIN dbo.Cars car ON j.CarId = car.Id AND car.IsDeleted = 0
                  left JOIN dbo.Columns co ON j.ColumnId = co.Id AND co.IsDeleted = 0
                  left JOIN dbo.Basements b ON co.BasementId = b.Id AND b.IsDeleted = 0
                  left JOIN dbo.Places p ON b.PlaceId = p.Id AND p.IsDeleted = 0
                  left JOIN dbo.Customers c ON car.CustomerId = c.Id AND c.IsDeleted = 0
                  left JOIN dbo.Workers w ON j.SlotInCharge = w.Id AND w.IsDeleted = 0
                  left JOIN dbo.UserProfile u On w.UserId = u.Id AND u.IsDeleted = 0
				      left join dbo.Subscriptions s on s.Id = j.SubscriptionId and s.IsDeleted = 0
				      left join dbo.Slots sl on sl.id = j.slotincharge and sl.isdeleted =0
				      left join dbo.slots sl1 on sl1.id = j.SlotSupport and sl1.IsDeleted = 0
                  left join dbo.Company com on com.Id = j.CompanyId and com.IsDeleted = 0";

                if (!string.IsNullOrEmpty(request.LicensePlates)
                    || !string.IsNullOrEmpty(request.PlaceName)
                    || !string.IsNullOrEmpty(request.Phone)
                    || request.ToDate != null
                    || request.FromDate != null)
                {
                    query += " Where j.IsDeleted = 0 AND (j.CompanyId = @CompanyId OR j.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))) AND ";
                    var first = true;
                    if (request.FromDate != null)
                    {
                        query += " j.BookJobDate >= @FromDate";
                        first = !first;
                    }
                    if (request.ToDate != null)
                    {
                        if (first)
                        {
                            query += " j.BookJobDate <= @ToDate";
                            first = !first;
                        }
                        else
                        {
                            query += " and j.BookJobDate <= @ToDate";
                        }

                    }
                    if (!string.IsNullOrEmpty(request.LicensePlates))
                    {
                        if (first)
                        {
                            query += " car.LicensePlates like N'%'+ @LicensePlates + '%'";
                            first = !first;
                        }
                        else
                        {
                            query += " and car.LicensePlates like N'%' + @LicensePlates + '%'";
                        }
                    }
                    if (!string.IsNullOrEmpty(request.PlaceName))
                    {
                        if (first)
                        {
                            query += " p.Title like '%' + @PlaceName + '%'";
                            first = !first;
                        }
                        else
                        {
                            query += " and p.Title like '%' + @PlaceName + '%'";
                        }

                    }
                }
                var lstJobs = _dapperRepository.QueryMultipleWithParam<GetJobResponseModel>(query, p);
                var jobsEnumerable = lstJobs.ToList();
                var total = jobsEnumerable.Count;
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstJobs = jobsEnumerable.OrderBy(request);
                }
                else
                {
                    lstJobs = jobsEnumerable.OrderByDescending(x => x.Id);
                }

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstJobs = lstJobs.Paging(request);
                }


                if (null == lstJobs)
                {
                    return new PagedResults<GetJobResponseModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<GetJobResponseModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUserService.CompanyId);
                var lstJobs = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspJobs_Suggestion", p);

                if (null == lstJobs)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstJobs,
                    TotalRecords = lstJobs.Count()
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> JobCheckoutAsync(CheckoutModel model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var entity = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == model.JobId && x.JobStatus == JobStatusEnum.IN_PROGRESS.ToString());
                if (null == entity)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                        ResponseMessage = Constants.ErrorMessageCodes.CarNotProcess,
                    };
                }

                entity.MaterialsUsed = model.MaterialsUsed;
                entity.ChemicalUsed = model.ChemicalUsed;
                entity.EndTime = DateTime.Now;
                entity.JobStatus = JobStatusEnum.DONE.ToString();
                entity.MainPhotoAfterWiping = await _uploadFileServices.UploadFileAsync(model.MainPhotoAfterWiping).ConfigureAwait(false);
                entity.MainPhotoBeforeWiping = await _uploadFileServices.UploadFileAsync(model.MainPhotoBeforeWiping).ConfigureAwait(false);
                entity.SubPhotoAfterWiping = await _uploadFileServices.UploadFileAsync(model.SubPhotoAfterWiping).ConfigureAwait(false);
                entity.TheSecondaryPhotoBeforeWiping = await _uploadFileServices.UploadFileAsync(model.TheSecondaryPhotoBeforeWiping).ConfigureAwait(false);
                _unitOfWork.JobsRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.UpdateSuccessMessage,
                    Data = true
                }; ;
            }
            catch (Exception e)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobCheckout Error: " + e.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> TeamLeadCommentsAsync(CommentModel model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var entity = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == model.JobId);
                if (null == entity)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                        ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                    };
                }

                entity.TeamLeadAssessment = model.Comments;
                entity.TeamLeadScore = model.Score;
                _unitOfWork.JobsRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.UpdateSuccessMessage,
                    Data = true
                }; ;
            }
            catch (Exception e)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Team Lead Comment Error: " + e.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> CustomerCommentsAsync(CommentModel model)
        {

            try
            {
                await _unitOfWork.OpenTransaction();
                var entity = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == model.JobId);
                if (null == entity)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                        ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                    };
                }

                entity.CustomerAssessment = model.Comments;
                entity.CustomerScore = model.Score;
                _unitOfWork.JobsRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.UpdateSuccessMessage,
                    Data = true
                }; ;
            }
            catch (Exception e)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Customer Comment Error: " + e.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(SaveJobModel entity, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var job = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == entity.Id && !x.IsDeleted);

                var attachReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                var attachOriginalName = _uploadFileServices.GetFileNameMultipleFileOriginalNameAsync(files);
                if (job != null && job.Id > 0)
                {
                    var checkDuplicate = _unitOfWork.JobsRepository.GetSingle(x => x.CarId == entity.CarId && x.BookJobDate.Value.Date == entity.BookJobDate.Value.Date && x.Id != entity.Id && !x.IsDeleted);
                    if (checkDuplicate != null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = "Xe đã được tạo job vào ngày " + entity.BookJobDate?.ToString(Constants.ddMMyyyy) + ", vui lòng kiểm tra lại",
                            Data = true
                        };
                    }

                    var fileNameOld = job.AttachmentFileReName ?? string.Empty;
                    job.BookJobDate = entity.BookJobDate;
                    job.ColumnId = entity.ColumnId;
                    job.CarId = entity.CarId;
                    job.SlotSupport = entity.SlotSupport;
                    job.StaffId = entity.StaffId;
                    job.StaffAssessment = entity.StaffAssessment;
                    job.StaffScore = entity.StaffScore;
                    job.TeamLeadAssessment = entity.TeamLeadAssessment;
                    job.TeamLeadScore = entity.TeamLeadScore;
                    job.AttachmentFileOriginalName = attachOriginalName;
                    job.AttachmentFileReName = attachReName;
                    job.JobStatus = entity.JobStatus;

                    _unitOfWork.JobsRepository.Update(job);
                    var update = await _unitOfWork.JobsRepository.CommitAsync().ConfigureAwait(false);
                    Log.Fatal("JobsRepository");
                    if (update)
                    {
                        Log.Fatal("UploadMultipleFile - UpLoad");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, fileNameOld);
                    }
                }
                else
                {
                    var checkDuplicate = _unitOfWork.JobsRepository.GetSingle(x => x.CarId == entity.CarId && x.BookJobDate.Value.Date == entity.BookJobDate.Value.Date && !x.IsDeleted);
                    if (checkDuplicate != null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = "Xe đã được tạo job vào ngày " + entity.BookJobDate?.ToString(Constants.ddMMyyyy) + ", vui lòng kiểm tra lại",
                            Data = false
                        };
                    }
                    var subscription = await _unitOfWork.SubscriptionsRepository
                        .GetSingleAsync(x => x.CarId == entity.CarId);

                    if (subscription == null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = "Xe chưa đăng ký dịch của công ty. Xin vui lòng liên hệ nhân viên công ty để được hỗ trợ.",
                            Data = false
                        };
                    }
                    if (!entity.BookJobDate.HasValue)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = "Vui lòng nhập ngày đặt lịch. ",
                            Data = false
                        };
                    }

                    if (subscription.EndDate.HasValue && subscription.EndDate.Value.Date < entity.BookJobDate.Value.Date)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = $"Gói dịch của bạn đã hết hạn vào ngày {subscription.EndDate.Value.Date.ToString(Constants.ddMMyyyy) }. Xin vui lòng liên hệ nhân viên công ty để được hỗ trợ."
                        };
                    }
                    var model = new Jobs
                    {
                        SlotInCharge = entity.SlotInCharge,
                        SlotSupport = entity.SlotSupport,
                        BookJobDate = entity.BookJobDate,
                        CarId = entity.CarId,
                        ColumnId = entity.ColumnId,
                        SubscriptionId = subscription?.Id ?? 0,
                        JobStatus = JobStatusEnum.TODO.ToString()
                    };
                    _unitOfWork.JobsRepository.Add(model);
                    Log.Fatal("JobsRepository");
                    var insert = await _unitOfWork.JobsRepository.CommitAsync().ConfigureAwait(false);
                    if (insert)
                    {
                        Log.Fatal("UploadMultipleFile New");
                        _uploadFileServices.UploadMultipleFileAsync(files, attachReName, string.Empty);
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
                Log.Fatal("JobsServices" + ex.Message);
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<long>> ManualAssignTaskAsync(Jobs model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var p = _modelUtility.ObjectUpdateToPrams(model);

                await _unitOfWork.JobsRepository.UpdateUsingDapper(model, "uspJobs_Update", p);
                _unitOfWork.CommitTransaction();
                return new PagedResult<long>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = model.Id
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<bool>> StartJobAsync(long jobId)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var old = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == jobId && x.JobStatus == JobStatusEnum.BOOKED.ToString());
                var slot = await _unitOfWork.SlotsRepository.GetSingleAsync(x => x.Id == old.SlotInCharge);
                if (old == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.JobExisted,
                        ResponseMessage = Constants.ErrorMessageCodes.JobExistedMessage,
                    };
                }
                old.JobStatus = JobStatusEnum.IN_PROGRESS.ToString();
                old.StartingTime = DateTime.Now;
                slot.NumberOfBonuses = slot.NumberOfBonuses == null ? 1 : slot.NumberOfBonuses + 1;
                _unitOfWork.JobsRepository.Update(old);
                var id = await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    Data = true,
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Book Job Error: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> BookJobAsync(long jobId, long slotId)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var old = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.Id == jobId && x.JobStatus == JobStatusEnum.TODO.ToString());
                if (old == null)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.JobExisted,
                        ResponseMessage = Constants.ErrorMessageCodes.JobHasCheckedMessage,
                    };
                }
                old.JobStatus = JobStatusEnum.BOOKED.ToString();
                old.SlotInCharge = slotId;
                _unitOfWork.JobsRepository.Update(old);
                var id = await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    Data = true,
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Book Job Error: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<int>> NewJobAsync(BookJobRequestModel model)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var old = await _unitOfWork.JobsRepository.GetSingleAsync(x => x.BookJobDate.Value.Date == model.BookJobDate.Date);
                if (old != null)
                {
                    return new PagedResult<int>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.JobExisted,
                        ResponseMessage = Constants.ErrorMessageCodes.JobExistedMessage,
                    };
                }
                var entity = _mapper.Map<Jobs>(model);
                if (!string.IsNullOrEmpty(model.LicensePlate))
                {
                    var car = await _unitOfWork.CarsRepository.GetSingleAsync(x => x.LicensePlates == model.LicensePlate);
                    if (car == null)
                    {
                        return new PagedResult<int>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.LicensePlateNotFound,
                            ResponseMessage = Constants.ErrorMessageCodes.LicensePlateNotFoundMessage,
                        };
                    }
                    entity.CarId = car.Id;
                    var sub = await _unitOfWork.SubscriptionsRepository.GetSingleAsync(x => x.CarId == car.Id);
                    if (sub == null)
                    {
                        return new PagedResult<int>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.SubNotFound,
                            ResponseMessage = Constants.ErrorMessageCodes.SubNotFoundMessage,
                        };
                    }
                    entity.SubscriptionId = sub.Id;
                    entity.StaffId = (await _unitOfWork.StaffsRepository
                        .GetSingleAsync(x => x.UserId == _authenticatedUserService.UserId))?.Id;
                }
                entity.JobStatus = JobStatusEnum.TODO.ToString();

                _unitOfWork.JobsRepository.Add(entity);
                var id = await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return new PagedResult<int>
                {
                    Data = id,
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " Book Job Error: " + ex.Message);
                throw;
            }

        }

        public async Task<IPagedResult<bool>> UpdateStatusAsync(string statusCode, long jobId)
        {
            try
            {
                var job = await _unitOfWork.JobsRepository.GetSingleAsync(z => z.Id == jobId);
                if (null == job)
                {
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.RecordNotFound,
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                        Data = false
                    };
                }

                job.JobStatus = statusCode;

                _unitOfWork.JobsRepository.Update(job);
                await _unitOfWork.SaveChangesAsync();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " UpdateStatus: " + ex.ToString());
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionJobColumnAsync(string searchText, long? placeId, long? basementId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.PlaceId, placeId ?? 0);
                p.Add(Constants.BasementId, basementId ?? 0);
                p.Add(Constants.CompanyId, _authenticatedUserService.CompanyId);
                var lstColumns = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspJobColumns_Suggestion", p);

                if (null == lstColumns)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstColumns.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<GetJobBySlotModel>> GetJobBySlotAsync(long slotId)
        {
            var slot = await _unitOfWork.SlotsRepository.GetSingleAsync(x => x.Id == slotId);
            if (slot == null)
            {
                return new PagedResults<GetJobBySlotModel>
                {
                    ResponseCode = Constants.ErrorMessageCodes.RecordNotFound,
                    ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage
                };
            }
            var query = @"SELECT j.Id, c.LicensePlates, j.BookJobDate, j.Title
            FROM     dbo.Jobs j INNER JOIN
                              dbo.ZoneColumn zc ON j.ColumnId = zc.ColumnId INNER JOIN
                              dbo.Cars c On j.carId = c.Id  INNER JOIN
                              dbo.Zones z ON  zc.ZoneId = z.Id INNER JOIN
                              dbo.TeamZone tz ON z.Id = tz.ZoneId INNER JOIN
                              dbo.Teams t ON tz.TeamId = t.Id INNER JOIN
                              dbo.Slots s ON t.Id = s.TeamId
            where s.Id = @SlotId
            and CONVERT(VARCHAR(10), j.BookJobDate, 103) = CONVERT(VARCHAR(10), GETDATE(), 103) and JobStatus = 'TODO'";
            var dynamicParameterPlaceId = new DynamicParameters();
            dynamicParameterPlaceId.Add("@SlotId", slotId);
            var listInfoPlace = _dapperRepository.QueryMultipleWithParam<GetJobBySlotModel>(query, dynamicParameterPlaceId);
            listInfoPlace = listInfoPlace.DistinctBy(x => x.Id, null);
            return new PagedResults<GetJobBySlotModel>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                ListData = listInfoPlace,
                TotalRecords = listInfoPlace.Count()
            };
        }

        public async Task<IPagedResults<ItemModel>> SuggestionCarByPlaceAsync(string searchText, long? placeId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.PlaceId, placeId);
                p.Add(Constants.CompanyId, _authenticatedUserService.CompanyId);
                var listCar = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspCar_SuggestionByPlace", p);

                if (null == listCar)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = listCar.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " JobsServices: " + ex.Message);
                throw;
            }
        }


    }
}
