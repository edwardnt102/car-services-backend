using Common.Pagging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel;
using ViewModel.RequestModel.Jobs;

namespace Services.Interfaces
{
    public interface IJobsServices
    {
        IPagedResults<GetJobResponseModel> GetAllAsync(GetJobREquestModel request);
        Task<IPagedResult<bool>> SaveAsync(SaveJobModel entity, List<IFormFile> files);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        Task<IPagedResult<int>> NewJobAsync(BookJobRequestModel model);
        Task<IPagedResult<bool>> CustomerCommentsAsync(CommentModel model);
        Task<IPagedResult<bool>> TeamLeadCommentsAsync(CommentModel model);
        Task<IPagedResult<bool>> JobCheckoutAsync(CheckoutModel model);
        Task<IPagedResult<bool>> StartJobAsync(long jobId);
        Task<IPagedResult<bool>> BookJobAsync(long jobId, long slotId);
        Task<IPagedResults<ItemModel>> SuggestionJobColumnAsync(string searchText, long? placeId, long? basementId);
        Task<IPagedResults<GetJobBySlotModel>> GetJobBySlotAsync(long slotId);
        Task<IPagedResults<ItemModel>> SuggestionCarByPlaceAsync(string searchText, long? placeId);

    }
}
