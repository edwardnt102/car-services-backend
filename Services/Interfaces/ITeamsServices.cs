using Common.Pagging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel.Teams;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface ITeamsServices
    {
        Task<IPagedResults<TeamViewModel>> GetAllAsync(PagingRequest request);
        Task<IPagedResult<bool>> SaveAsync(TeamSaveModel entity, List<IFormFile> files);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        IPagedResults<ItemModel> SuggestionZoneByPlaceAsync(string searchText, string listTeamPlace);
        IPagedResults<ItemModel> SuggestionWorkerByPlaceAsync(string searchText, string listTeamPlace);
    }
}
