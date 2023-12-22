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
    public class ProvinceServices : IProvinceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;

        public ProvinceServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IModelUtility modelUtility)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
        }

        public async Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Province> lstProvince)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (lstProvince == null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                            ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                        };
                    }

                    var p = new DynamicParameters();
                    p.Add("@items", CreateProvinceDataTable(lstProvince));
                    await _unitOfWork.ProvinceRepository.BulkInsertUsingDapper("uspProvince_bulkInsert", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProvinceServices: " + ex.Message);
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
            var Province = await _unitOfWork.ProvinceRepository.GetByIdUsingDapper(id, "uspProvince_selectById");
            if (null == Province)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.ProvinceNotExisted,
                    ResponseMessage = Constants.ErrorMessageCodes.ProvinceNotExistedMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    await _unitOfWork.ProvinceRepository.DeleteUsingDapper(id, "uspProvince_Delete");
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProvinceServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResults<Province>> GetAllAsync(PagingRequest request)
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
                var lstProvince = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Province>("uspProvince_selectAll", p, 30);
                var total = lstProvince.Count();
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstProvince = lstProvince.OrderBy(request);
                }


                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstProvince = lstProvince.Paging(request);
                }


                if (null == lstProvince)
                {
                    return new PagedResults<Province>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<Province>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstProvince,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProvinceServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = "";
                }
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstProvince = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspProvince_Suggestion", p, 30);


                if (null == lstProvince)
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
                    ListData = lstProvince,
                    TotalRecords = lstProvince.Count()
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProvinceServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InsertAsync(Province model)
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
                    var id = await _unitOfWork.ProvinceRepository.InsertUsingDapper("uspProvince_Insert", p);

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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProvinceServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResult<bool>> UpdateAsync(Province model)
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

                    var id = await _unitOfWork.ProvinceRepository.UpdateUsingDapper(model, "uspProvince_Update", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProvinceServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        private object CreateProvinceDataTable(IEnumerable<Province> provinceList)
        {
            var data = DataTableUtility.ListObjectToDataTable(provinceList, "Province");
            return data.AsTableValuedParameter();
        }
    }
}
