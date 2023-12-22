using Common.Pagging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel.Zone;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IZonesServices
    {
        Task<IPagedResults<ZoneViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> SaveAsync(ZoneSaveModel entity, List<IFormFile> files);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResults<ItemModel>> SuggestionBasementAsync(string searchText, long placeId);
        Task<IPagedResults<ItemModel>> SuggestionColumnAsync(string searchText, long basementId);

    }
}
