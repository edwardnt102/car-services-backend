using Dapper;
using Repository.Enum;
using Repository.ExceptionTypes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Utility.Configuration;

namespace Repository
{
    public class DapperRepository : IDapperRepository
    {
        public static string ConnectionString = Configuration.Instance.GetConfig<string>("ConnectionStrings", "DB5P10B");

        #region Command
        #region Query Multiple and exact mapping
        public IEnumerable<T> QueryMultipleWithParam<T>(string queryString, object prm)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var multi = connection.QueryMultiple(queryString, prm))
                    {
                        var result = multi.Read<T>();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }

        }
        public IEnumerable<T> QueryMultiple<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var multi = connection.QueryMultiple(queryString, commandTimeout: commandTimeOut))
                    {
                        var result = multi.Read<T>();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }

        }
        public async Task<IEnumerable<T>> QueryMultipleUsingStoreProcAsync<T>(string spName, object parms = null, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(spName, parms, commandTimeout: commandTimeOut, commandType: CommandType.StoredProcedure))
                    {
                        var result = multi.Read<T>();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }

        public List<T> GetBySql<T>(string sql)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            return db.Query<T>(sql).ToList();
        }
        #endregion

        #region Query FirstOrDefault
        public T QueryFirstOrDefault<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    return connection.QueryFirstOrDefault<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        public T QueryFirstOrDefault<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.QueryFirstOrDefault<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        public T QueryFirstOrDefaultUsingStoreProc<T>(string spName, object parms)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.QueryFirstOrDefault<T>(spName, parms, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        #endregion

        #region Execute Scalar
        public T ExecuteScalar<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.ExecuteScalar<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        public T ExecuteScalar<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    return connection.ExecuteScalar<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        public T ExecuteScalarUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.ExecuteScalar<T>(spName, parms, commandTimeout: commandTimeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        #endregion

        #region Insert/Update
        public T InsertOrUpdateUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return connection.Query<T>(spName, parms, commandTimeout: commandTimeOut, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }

        public async Task UpdateMultiple(string connectionString, string executeString, List<dynamic> listDataUpDate, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.ExecuteAsync(executeString, listDataUpDate, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }

        }
        #endregion

        #region Execute To DataTable
        public DataTable ExecuteToDataTable(string queryString, int commandTimeOut = 30)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var reader = connection.ExecuteReader(queryString, commandTimeout: commandTimeOut);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;

                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }

        public DataTable ExecuteToDataTable(string connectionString, string queryString, int commandTimeOut = 30)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var reader = connection.ExecuteReader(queryString, commandTimeout: commandTimeOut);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;

                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }
        #endregion
        #endregion

        #region Bulk Copy
        public async Task DataTableBulkInsert(string connectionString, DataTable dataTable,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize,
            bool isKeepIdentity = false)
        {
            try
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString, isKeepIdentity == true ? SqlBulkCopyOptions.KeepIdentity : SqlBulkCopyOptions.Default))
                {
                    sqlBulkCopy.DestinationTableName = dataTable.TableName;
                    sqlBulkCopy.BulkCopyTimeout = bulkCopyTimeOut;
                    sqlBulkCopy.BatchSize = batchSize;
                    MapColumns(dataTable, sqlBulkCopy);
                    await sqlBulkCopy.WriteToServerAsync(dataTable);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }

        //TODO: Implement Bulk Update.

        public async Task DataReaderBulkInsert(string connectionString,
            IDataReader dataReader,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection))
                    {
                        sqlBulkCopy.DestinationTableName = dataReader.GetSchemaTable().TableName;
                        sqlBulkCopy.BulkCopyTimeout = bulkCopyTimeOut;
                        sqlBulkCopy.BatchSize = batchSize;
                        await sqlBulkCopy.WriteToServerAsync(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }

        }

        public async Task UpdateUsingQuery(string executeString, object parms)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.ExecuteAsync(executeString, parms, commandTimeout: (int)CommandHelperEnum.CommainTimeOut).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.ToString());
                throw new DbException(ex.ToString());
            }
        }

        private SqlBulkCopy MapColumns(DataTable infoTable, SqlBulkCopy bulkCopy)
        {

            foreach (DataColumn dc in infoTable.Columns)
            {
                bulkCopy.ColumnMappings.Add(dc.ColumnName,
                  dc.ColumnName);
            }

            return bulkCopy;
        }
        #endregion
    }
}
