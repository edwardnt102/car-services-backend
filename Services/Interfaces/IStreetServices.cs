using Common.Pagging;
using Entity.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IStreetServices
    {
        Task<IPagedResults<Street>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> InsertAsync(Street entity);
        Task<IPagedResult<bool>> UpdateAsync(Street entity);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Street> lstStreet);
    }
}
