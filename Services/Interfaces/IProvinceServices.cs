using System.Collections.Generic;
using Common.Pagging;
using Entity.Model;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IProvinceServices
    {
        Task<IPagedResults<Province>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> InsertAsync(Province entity);
        Task<IPagedResult<bool>> UpdateAsync(Province entity);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<Province> lstProvince);
    }
}
