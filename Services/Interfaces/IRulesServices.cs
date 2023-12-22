using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IRulesServices
    {
        Task<IPagedResults<RuleViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> SaveAsync(RuleRequest entity, List<IFormFile> files);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<Rules>> GetDetailAsync(long id);
    }
}
