using Repository.Enum;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDapperRepository
    {
        IEnumerable<T> QueryMultipleWithParam<T>(string queryString, object prm);
        IEnumerable<T> QueryMultiple<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        Task<IEnumerable<T>> QueryMultipleUsingStoreProcAsync<T>(string spName, object parms = null, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        List<T> GetBySql<T>(string sql);
        T QueryFirstOrDefault<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        T QueryFirstOrDefault<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        T QueryFirstOrDefaultUsingStoreProc<T>(string spName, object parms);
        T ExecuteScalar<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        T ExecuteScalar<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        T ExecuteScalarUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        T InsertOrUpdateUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        Task UpdateMultiple(string connectionString, string executeString, List<dynamic> listDataUpDate, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);
        DataTable ExecuteToDataTable(string queryString, int commandTimeOut = 30);
        DataTable ExecuteToDataTable(string connectionString, string queryString, int commandTimeOut = 30);
        Task DataTableBulkInsert(string connectionString, DataTable dataTable,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize,
            bool isKeepIdentity = false);
        Task DataReaderBulkInsert(string connectionString,
            IDataReader dataReader,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize);

        Task UpdateUsingQuery(string executeString, object parms);
    }
}
