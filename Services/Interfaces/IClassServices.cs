using Common.Pagging;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IClassServices
    {
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
    }
}
