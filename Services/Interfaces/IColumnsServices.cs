using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using ViewModel.ListBoxModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IColumnsServices
    {
        Task<IPagedResults<ColumnViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> SaveAsync(Columns entity, List<IFormFile> files);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        
    }
}
