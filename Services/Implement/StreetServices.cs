using Common;
using Common.Constants;
using Common.Pagging;
using Dapper;
using Entity.Model;
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

namespace Services.Implement
{
    public class StreetServices : IStreetServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;
        public StreetServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IModelUtility modelUtility)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
        }

        public async Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Street> lstStreet)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (lstStreet == null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                            ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                        };
                    }

                    var p = new DynamicParameters();
                    p.Add("@items", CreateStreetDataTable(lstStreet));
                    await _unitOfWork.StreetRepository.BulkInsertUsingDapper("uspStreet_bulkInsert", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetServices: " + ex.Message);
                    throw;

                }
            }
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
            var Street = await _unitOfWork.StreetRepository.GetByIdUsingDapper(id, "uspStreet_selectById");
            if (null == Street)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.StreetNotExisted,
                    ResponseMessage = Constants.ErrorMessageCodes.StreetNotExistedMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    await _unitOfWork.StreetRepository.DeleteUsingDapper(id, "uspStreet_Delete");
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResults<Street>> GetAllAsync(PagingRequest request)
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
                if (string.IsNullOrWhiteSpace(request.SearchText))
                {
                    request.SearchText = string.Empty;
                }
                p.Add(Constants.Key, request.SearchText);
                var lstStreet = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Street>("uspStreet_selectAll", p, 30);
                var listData = lstStreet.ToList();
                var total = listData.Count();
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstStreet = listData.OrderBy(request);
                }


                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstStreet = listData.Paging(request);
                }


                if (null == lstStreet)
                {
                    return new PagedResults<Street>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<Street>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = listData,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstStreet = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspStreet_Suggestion", p, 30);


                if (null == lstStreet)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstStreet.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InsertAsync(Street model)
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
                    var id = await _unitOfWork.StreetRepository.InsertUsingDapper("uspStreet_Insert", p);

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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResult<bool>> UpdateAsync(Street model)
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

                    var id = await _unitOfWork.StreetRepository.UpdateUsingDapper(model, "uspStreet_Update", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        private object CreateStreetDataTable(IEnumerable<Street> StreetList)
        {
            var data = DataTableUtility.ListObjectToDataTable(StreetList, "Street");
            return data.AsTableValuedParameter();
        }
    }
}
