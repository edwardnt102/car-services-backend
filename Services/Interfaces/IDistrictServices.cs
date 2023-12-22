using System.Collections.Generic;
using Common.Pagging;
using Entity.Model;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IDistrictServices
    {
        Task<IPagedResults<District>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> InsertAsync(District entity);
        Task<IPagedResult<bool>> UpdateAsync(District entity);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText, long provinceId);
        Task<IPagedResult<bool>> BulkInsertAsync(IEnumerable<District> lstDistrict);
    }
}
