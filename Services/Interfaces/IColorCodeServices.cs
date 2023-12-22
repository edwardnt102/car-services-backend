using System.Threading.Tasks;
using Common.Pagging;
using ViewModel.ListBoxModel;

namespace Services.Interfaces
{
    public interface IColorCodeServices
    {
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
    }
}
