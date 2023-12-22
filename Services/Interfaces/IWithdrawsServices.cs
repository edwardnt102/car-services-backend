using Common.Pagging;
using Entity.Model;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IWithdrawsServices
    {
        Task<IPagedResults<Withdraws>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> InsertAsync(Withdraws entity);
        Task<IPagedResult<bool>> UpdateAsync(Withdraws entity);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
    }
}
