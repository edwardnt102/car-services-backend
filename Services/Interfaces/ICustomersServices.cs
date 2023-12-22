using Common.Pagging;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface ICustomersServices
    {
        Task<IPagedResults<CustomerViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResults<CustomerCarViewModel>> GetInformationForCustomerAsync(string userName);
        Task<IPagedResult<bool>> InActiveUserAsync(string userId, bool active);
    }
}
