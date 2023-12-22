using Common.Pagging;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IWorkersServices
    {
        Task<IPagedResults<WorkerViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<bool>> InActiveUserAsync(string userId, bool active);
        Task<IPagedResult<decimal>> CheckWalletAsync();
        IPagedResults<ItemModel> SuggestionWorkerTypeAsync();

    }
}
