using Common;
using Common.Constants;
using Common.Pagging;
using Dapper;
using Entity.Model;
using Repository;
using Serilog;
using Services.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utility;
using ViewModel.ListBoxModel;

namespace Services.Implement
{
    public class WithdrawsServices : IWithdrawsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;

        public WithdrawsServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IModelUtility modelUtility)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
        }

        public async Task<IPagedResult<bool>> DeleteAsync(long id)
        {

            if (string.IsNullOrEmpty(id.ToString()))
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.IsNullOrEmpty,
                    ResponseMessage = Constants.ErrorMessageCodes.IsNullOrEmptyMessage,
                    Data = false
                };
            }
            var Withdraws = await _unitOfWork.WithdrawsRepository.GetByIdUsingDapper(id, "uspWithdraws_selectById");
            if (null == Withdraws)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.WithdrawsNotExisted,
                    ResponseMessage = Constants.ErrorMessageCodes.WithdrawsNotExistedMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    await _unitOfWork.WithdrawsRepository.DeleteUsingDapper(id, "uspWithdraws_Delete");
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResults<Withdraws>> GetAllAsync(PagingRequest request)
        {
            try
            {
                if (null == request)
                {
                    request = new PagingRequest
                    {
                        Page = 1,
                        PageSize = 10,
                        SortDir = Constants.SortDesc,
                        SortField = Constants.SortId
                    };
                }
                var p = new DynamicParameters();
                p.Add(Constants.Key, request.SearchText);
                var lstWithdraws = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Withdraws>("uspWithdraws_selectAll", p, 30);
                var total = lstWithdraws.Count();
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstWithdraws = lstWithdraws.OrderBy(request);
                }
                else
                {
                    lstWithdraws = lstWithdraws.OrderByDescending(x => x.Id);
                }

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstWithdraws = lstWithdraws.Paging(request);
                }


                if (null == lstWithdraws)
                {
                    return new PagedResults<Withdraws>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<Withdraws>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstWithdraws,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstWithdraws = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspWithdraws_Suggestion", p, 30);


                if (null == lstWithdraws)
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
                    ListData = lstWithdraws,
                    TotalRecords = lstWithdraws.Count()
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InsertAsync(Withdraws model)
        {
            if (model == null)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                    ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var p = _modelUtility.ObjectCreateToPrams(model);
                    var id = await _unitOfWork.WithdrawsRepository.InsertUsingDapper("uspWithdraws_Insert", p);

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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResult<bool>> UpdateAsync(Withdraws model)
        {
            if (model == null)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                    ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var p = _modelUtility.ObjectUpdateToPrams(model);

                    var id = await _unitOfWork.WithdrawsRepository.UpdateUsingDapper(model, "uspWithdraws_Update", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
