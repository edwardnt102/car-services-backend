using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface ICarModelsServices
    {
        Task<IPagedResults<CarModelViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> SaveAsync(CarModels entity, List<IFormFile> files, List<IFormFile> modelImage);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
    }
}
