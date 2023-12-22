using Common.Pagging;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IStaffsServices
    {
        Task<IPagedResults<StaffViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<bool>> InActiveUserAsync(string userId, bool active);
    }
}
