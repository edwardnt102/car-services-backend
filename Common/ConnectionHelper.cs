using Microsoft.Extensions.Configuration;
using System;

namespace Common
{
    public static class ConnectionHelper
    {
        public enum DatabaseName
        {
            DB5P10B,
            AuthenticationDB
        }

        //public static SQL GetConnection(DatabaseName dbName, IConfiguration config)
        //{
        //    return new SqlserverConnection(GetConnectionString(dbName, config));
        //}

        private static string GetConnectionString(DatabaseName dbName, IConfiguration config)
        {
            var connectionString = config.GetConnectionString(dbName.ToString());
            if (connectionString == null)
                throw new Exception($"Connection string {dbName} not in config");

            return connectionString;
        }
    }
}
