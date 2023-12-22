using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IBasementsServices
    {
        Task<IPagedResults<BasementViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText, long? placeId);
        Task<IPagedResult<bool>> SaveAsync(Basements model, IFormFile file, List<IFormFile> files);
        
    }
}
