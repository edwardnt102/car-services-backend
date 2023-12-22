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
    public class WardServices : IWardServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;

        public WardServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IModelUtility modelUtility)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
        }

        public async Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Ward> lstWard)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (lstWard == null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                            ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                        };
                    }
                    var p = new DynamicParameters();
                    p.Add("@items", CreateWardDataTable(lstWard));
                    await _unitOfWork.WardRepository.BulkInsertUsingDapper("uspWard_bulkInsert", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WardServices: " + ex.Message);
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
            var Ward = await _unitOfWork.WardRepository.GetByIdUsingDapper(id, "uspWard_selectById");
            if (null == Ward)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.WardNotExisted,
                    ResponseMessage = Constants.ErrorMessageCodes.WardNotExistedMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    await _unitOfWork.WardRepository.DeleteUsingDapper(id, "uspWard_Delete");
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WardServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResults<Ward>> GetAllAsync(PagingRequest request)
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
                var lstWard = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Ward>("uspWard_selectAll", p, 30);
                var listData = lstWard.ToList();
                var total = listData.Count();
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstWard = listData.OrderBy(request);
                }

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstWard = listData.Paging(request);
                }


                if (null == lstWard)
                {
                    return new PagedResults<Ward>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<Ward>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = listData,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WardServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstWard = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspWard_Suggestion", p, 30);


                if (null == lstWard)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstWard.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WardServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InsertAsync(Ward model)
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
                    var id = await _unitOfWork.WardRepository.InsertUsingDapper("uspWard_Insert", p);

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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WardServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResult<bool>> UpdateAsync(Ward model)
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

                    var id = await _unitOfWork.WardRepository.UpdateUsingDapper(model, "uspWard_Update", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WardServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        private object CreateWardDataTable(IEnumerable<Ward> wardList)
        {
            var data = DataTableUtility.ListObjectToDataTable(wardList, "Ward");
            return data.AsTableValuedParameter();
        }
    }
}
