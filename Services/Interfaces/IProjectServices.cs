using System.Collections.Generic;
using Common.Pagging;
using Entity.Model;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IProjectServices
    {
        Task<IPagedResults<Project>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> InsertAsync(Project entity);
        Task<IPagedResult<bool>> UpdateAsync(Project entity);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText, long provinceId, long districtId);
        Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Project> lstProject);
    }
}
