using Common.Constants;
using Common.Pagging;
using Dapper;
using Repository;
using Serilog;
using Services.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViewModel.ListBoxModel;
namespace Services.Implement
{
    public class ClassServices : IClassServices
    {
        private readonly IDapperRepository _dapperRepository;

        public ClassServices(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstClass = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspClass_Suggestion", p);

                if (null == lstClass)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstClass.ToList();
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = itemModels,
                    TotalRecords = itemModels.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ClassServices: " + ex.Message);
                throw;
            }
        }
    }
}
