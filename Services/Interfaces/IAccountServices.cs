using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.RequestModel.Account;
using ViewModel.RequestModel.Customer;
using ViewModel.RequestModel.Staff;
using ViewModel.RequestModel.Worker;

namespace Services.Interfaces
{
    public interface IAccountServices
    {
        Task<bool> CreateUserRoleAsync(string useName, string roleName);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteUserRoleAsync(string useName, string roleName);
        Task<AppUser> GetByIdAsync(string userId);
        Task<IPagedResult<AppUser>> AddCustomerAsync(AddCustomer model);
        Task<IPagedResult<AppUser>> UpdateCustomerAsync(UpdateCustomer model, List<IFormFile> files);
        Task<IPagedResult<AppUser>> AddStaffAsync(AddStaff model);
        Task<IPagedResult<AppUser>> UpdateStaffAsync(UpdateStaff model, List<IFormFile> files);
        Task<IPagedResult<AppUser>> AddWorkerAsync(AddWorker model);
        Task<IPagedResult<AppUser>> UpdateWorkerAsync(UpdateWorker model, List<IFormFile> files);
        Task<IPagedResult<string>> UpdateAvatarAsync(IFormFile files);
        Task<IPagedResult<bool>> UpdateProfileAsync(ProfileUpdateModel request);
        Task<IPagedResult<bool>> ChangePassword(ChangePasswordModel request);
        Task<IPagedResult<AppUser>> GetProfileDetail();
    }
}
