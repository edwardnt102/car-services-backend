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
    public class ProjectServices : IProjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IModelUtility _modelUtility;

        public ProjectServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IModelUtility modelUtility)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _modelUtility = modelUtility;
        }

        public async Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Project> lstProject)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (lstProject == null)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Constants.ErrorMessageCodes.ModelIsInvalid,
                            ResponseMessage = Constants.ErrorMessageCodes.ModelIsInvalidMessage,
                        };
                    }

                    var p = new DynamicParameters();
                    p.Add("@items", CreateProjectDataTable(lstProject));
                    await _unitOfWork.ProjectRepository.BulkInsertUsingDapper("uspProject_bulkInsert", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProjectServices: " + ex.Message);
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
            var Project = await _unitOfWork.ProjectRepository.GetByIdUsingDapper(id, "uspProject_selectById");
            if (null == Project)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Constants.ErrorMessageCodes.ProjectNotExisted,
                    ResponseMessage = Constants.ErrorMessageCodes.ProjectNotExistedMessage,
                    Data = false
                };
            }
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    await _unitOfWork.ProjectRepository.DeleteUsingDapper(id, "uspProject_Delete");
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProjectServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResults<Project>> GetAllAsync(PagingRequest request)
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
                var lstProject = await _dapperRepository.QueryMultipleUsingStoreProcAsync<Project>("uspProject_selectAll", p, 30);
                var total = lstProject.Count();
                if (!string.IsNullOrEmpty(request.SortField))
                {
                    lstProject = lstProject.OrderBy(request);
                }


                if (request.Page != null && request.PageSize.HasValue)
                {
                    lstProject = lstProject.Paging(request);
                }


                if (null == lstProject)
                {
                    return new PagedResults<Project>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                return new PagedResults<Project>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = lstProject,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProjectServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText, long provinceId, long districtId)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = "";
                }
                var result = new List<ItemModel>();
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add("@provinceId", provinceId);
                p.Add("@districtId", districtId);
                result.AddRange(await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspProject_Suggestion", p, 300));
                result.AddRange(await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspStreet_Suggestion", p, 300));
                result.AddRange(await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspWard_Suggestion", p, 300));

                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    ListData = result,
                    TotalRecords = result.Count()
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProjectServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> InsertAsync(Project model)
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
                    var id = await _unitOfWork.ProjectRepository.InsertUsingDapper("uspProject_Insert", p);

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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProjectServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        public async Task<IPagedResult<bool>> UpdateAsync(Project model)
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

                    var id = await _unitOfWork.ProjectRepository.UpdateUsingDapper(model, "uspProject_Update", p);
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
                    Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ProjectServices: " + ex.Message);
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        private object CreateProjectDataTable(IEnumerable<Project> projectList)
        {
            var data = DataTableUtility.ListObjectToDataTable(projectList, "Project");
            return data.AsTableValuedParameter();
        }
    }
}
