using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utility.Redis.StackExchangeRedis.Interfaces
{
    public interface IRedisCacheProvider
    {
        Task Set(string key, object value);
        Task Set(string key, object value, TimeSpan expireTime);
        Task<T> Get<T>(string key);
        List<RedisKey> GetKeys(string keyPattern);
        bool IsInCache(string key);
        Task<bool> Remove(string key);
    }
}
