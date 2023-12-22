using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel.Slots;
using ViewModel.ViewModels;

namespace Services.Interfaces
{
    public interface ISlotsServices
    {
        IPagedResults<SlotViewModel> GetAllAsync(RequestGetSlot request);
        Task<IPagedResult<bool>> SaveAsync(StaffUpdate entity, List<IFormFile> files);
        Task<IPagedResult<bool>> DeleteAsync(long id);
        Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText);
        IPagedResults<ItemModel> GetTeamsAsync(long? workerId);
        IPagedResults<ItemModel> GetPlacesAsync(long? teamId);
        Task<IPagedResult<bool>> CreateSlotAsync(CreateSlot createSlot);
        Task<IPagedResult<bool>> UpdateSlotAsync(UpdateSlot updateSlot);
        Task<IPagedResult<bool>> CancelSlotAsync(CancelSlot cancelSlot);
        Task<IPagedResult<bool>> CheckInAsync(IFormFile file);
        Task<IPagedResult<bool>> CheckOutAsync(CheckOut checkOut, IFormFile file);
        Task<IPagedResult<bool>> ApproveSlotAsync(long id);
        Task<IPagedResult<BonusSalary>> GetSlotSalaryAsync(long slotId);
        Task<IPagedResults<ItemModel>> SuggestionInternAsync(long slotId, string searchText);
        Task<IPagedResults<ItemModel>> SuggestionInChargeAsync(long workerId, DateTime bookJobDate, string searchText);
 
    }
}
