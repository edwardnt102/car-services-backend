using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface ICarsServices
    {
        Task<IPagedResults<CarViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> SaveAsync(Cars entity, List<IFormFile> files, List<IFormFile> carImage);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResults<ItemModel>> SuggestionCarModelByCarBrandAsync(string searchText, long brandId);
    }
}
