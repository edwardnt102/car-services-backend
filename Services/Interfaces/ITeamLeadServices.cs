using Common.Pagging;
using System;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel.Jobs;
using ViewModel.RequestModel.Slots;
using ViewModel.ResponseMessage;

namespace Services.Interfaces
{
    public interface ITeamLeadServices
    {
        IPagedResults<BookSlotList> GetBookSlotList(DateTime? date = null);
        IPagedResults<BookSlotList> GetBookSlotListByTeamLead(DateTime? date);
        IPagedResults<BookJobList> GetJobList();
        IPagedResults<GetJobBySlotModel> GetJobBySlot(string status = null);
        IPagedResults<GetJobBySlotModel> GetJobBySlotAsyncByTeamLead(long? slotId = null, string status = null);
        Task<IPagedResult<bool>> TeamLeadCommentsAsync(CommentModel model);
        Task<IPagedResults<BookJobList>> GetJobListByTeamLead(long workerId);
        PagedResults<TimeLineReportModel> TimeLineReportAsync(DateTime? dateTime);
        Task<IPagedResults<ItemModel>> GetWorkerInTeam();
        Task<IPagedResult<bool>> UpdateSlotAsync(WorkerUpdateSlot updateSlot);
        Task<IPagedResult<bool>> BookJobAsync(long jobId);
        Task<IPagedResult<bool>> BookJobAsyncByTeamLead(long jobId, long workerId);
        IPagedResults<TimeLineReportModel> TimeLineReportAsyncWorker(DateTime? dateTime);
    }
}
