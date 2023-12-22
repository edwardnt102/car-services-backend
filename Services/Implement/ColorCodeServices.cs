using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Constants;
using Common.Pagging;
using Dapper;
using Repository;
using Serilog;
using Services.Interfaces;
using ViewModel.ListBoxModel;

namespace Services.Implement
{
    public class ColorCodeServices : IColorCodeServices
    {
        private readonly IDapperRepository _dapperRepository;

        public ColorCodeServices(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                var lstColorCode = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspColorCode_Suggestion", p);

                if (null == lstColorCode)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstColorCode.ToList();
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
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ColorCodeServices: " + ex.Message);
                throw;
            }
        }
    }
}
