using Common.Pagging;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseMessage;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface IReportServices
    {
        IPagedResult<CoordinatorViewModel> CoordinatorAsync(long placeId, DateTime? day);
        IEnumerable<TimeLineReportModel> TimeLineReportAsync(DateTime? dateTime);
        Task<List<BasementDetail>> GetDetailAsync(long placeId);
        Task<ColumnDetail> GetDetailByColumnAsync(long basementId, string columnName);
        Task<IPagedResults<WorkerViewModel>> GetWorkerByTeamAsync(long? request);
        Task<IPagedResults<SlotViewModel>> GetSlotByPlaceAndDateAsync(PlaceDateRequest request);
        Task<IPagedResults<SlotViewModel>> GetSlotByPlaceTeamAndDateAsync(PlaceDateTeamRequest request);
        Task<IPagedResults<Jobs>> GetJobByPlateAndDateAsync(PlaceDateRequest request);
        Task<IPagedResults<Jobs>> GetJobByColIDAsync(long? colID);
        Task<IPagedResults<Jobs>> GetJobBySlotIDAsync(long? slotID);
        Task<IPagedResults<BasementViewModel>> GetAllBasementByPlaceAsync(long? placeID);
        Task<IPagedResults<PlaceTeamZoneViewModel>> GetAllPlaceAsync(PagingRequest request);
        Task<IPagedResults<CarViewReport>> GetCarByZoneAsync(long? zoneId);
        Task<IPagedResult<TeamReportViewModel>> ReportTeamAsync(long teamID, long placeId, DateTime? day);
        Task<IPagedResults<CustomerViewModelReporterViewModel>> GetInformationForCustomerReport(string userName);
    }
}
