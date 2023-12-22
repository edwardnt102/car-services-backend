using Common.Pagging;
using System.Threading.Tasks;
using ViewModel.Requests;

namespace Services.Interfaces
{
    public interface IImportExcelServices
    {
        Task<IPagedResult<bool>> UploadAsync(FileModel model);
    }
}
