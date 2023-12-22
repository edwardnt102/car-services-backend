using System.Collections.Generic;
using Common.Pagging;
using Entity.Model;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IWardServices
    {
        Task<IPagedResults<Ward>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> InsertAsync(Ward entity);
        Task<IPagedResult<bool>> UpdateAsync(Ward entity);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Ward> lstWard);
    }
}
