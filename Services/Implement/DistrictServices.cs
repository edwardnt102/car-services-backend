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
using System.Threading.Tasks;
using Utility;
using ViewModel.ListBoxModel;

namespace Services.Implement
{
    public class DistrictServices : IDistrictServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;

        public DistrictServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IModelUtility modelUtility)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
        }

        public async Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<District> lstDistrict)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (lstDistrict == null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                            ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                        };
                    }

                    var p = new DynamicParameters();
                    p.Add("@items", CreateDistrictDataTable(lstDistrict));
                    await _unitOfWork.DistrictRepository.BulkInsertUsingDapper("uspDistrict_bulkInsert", p);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.Success,
                        ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                        Data = true
                    };
                }
                catch (Exception ex)
                {
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictServices: " + ex.Message);
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
            var District = await _unitOfWork.DistrictRepository.GetByIdUsingDapper(id, "uspDistrict_selectById");
            if (null == District)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.DistrictNotExisted,
                    ResponseMessage = Constants.ErrorMessageCodes.DistrictNotExistedMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    await _unitOfWork.DistrictRepository.DeleteUsingDapper(id, "uspDistrict_Delete");
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.Success,
                        ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                        Data = true
                    };
                }
                catch (Exception ex)
                {
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResults<District>> GetAllAsync(PagingRequest request)
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
                    request.SearchText = "all";
                }
                p.Add(Constants.Key, request.SearchText);
                var lstDistrict = await _dapperRepository.QueryMultipleUsingStoreProcAsync<District>("uspDistrict_selectAll", p, 30);
                var listData = lstDistrict.ToList();
                var total = listData.Count();
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstDistrict = listData.OrderBy(request);
                }

                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstDistrict = listData.Paging(request);
                }


                if (null == lstDistrict)
                {
                    return new PagedResults<District>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.RecordNotFound,
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<District>
                {
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = listData,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText, long provinceId)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = "";
                }
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add("@provinceId", provinceId);
                var lstDistrict = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspDistrict_Suggestion", p, 30);


                if (null == lstDistrict)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.RecordNotFound,
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstDistrict.ToList();
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Constants.ErrorMessageCodes.Success,
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = itemModels,
                    TotalRecords = itemModels.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InsertAsync(District model)
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
                    var id = await _unitOfWork.DistrictRepository.InsertUsingDapper("uspDistrict_Insert", p);

                    _unitOfWork.CommitTransaction();

                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.Success,
                        ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                        Data = true
                    };
                }
                catch (Exception ex)
                {
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResult<bool>> UpdateAsync(District model)
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

                    var id = await _unitOfWork.DistrictRepository.UpdateUsingDapper(model, "uspDistrict_Update", p);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Constants.ErrorMessageCodes.Success,
                        ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                        Data = true
                    };
                }
                catch (Exception ex)
                {
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        private object CreateDistrictDataTable(IEnumerable<District> districtList)
        {
            var data = DataTableUtility.ListObjectToDataTable(districtList, "District");
            return data.AsTableValuedParameter();
        }
    }
}
