using StackExchange.Redis;
using System;

namespace Utility.Redis
{
    public class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;
        private static readonly string _redisIp;
        private static readonly string _redisPassword;
        private static readonly bool _redisAbortOnConnectFail;
        static RedisStore()
        {
            _redisIp = Configuration.Configuration.Instance.GetConfig<string>("Redis", "Ip");
            _redisPassword = Configuration.Configuration.Instance.GetConfig<string>("Redis", "Password");
            _redisAbortOnConnectFail = Configuration.Configuration.Instance.GetConfig<bool>("Redis", "AbortOnConnectFail");
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { _redisIp },
                Password = _redisPassword,
                AbortOnConnectFail = _redisAbortOnConnectFail
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
        public static IServer RedisServer => Connection.GetServer(_redisIp);
    }
}
