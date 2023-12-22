using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Redis.StackExchangeRedis.Interfaces;

namespace Utility.Redis.StackExchangeRedis.Implements
{
    public class RedisCacheProvider : IRedisCacheProvider
    {
        IDatabase _redisCache;
        IServer _redisServer;
        public RedisCacheProvider()
        {
            _redisCache = RedisStore.RedisCache;
            _redisServer = RedisStore.RedisServer;
        }

        public async Task Set(string key, object value)
        {
            await _redisCache.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }
        public async Task Set(string key, object value, TimeSpan expireTime)
        {
            await _redisCache.StringSetAsync(key, JsonConvert.SerializeObject(value), expireTime);
        }
        public async Task<T> Get<T>(string key)
        {
            var value = await _redisCache.StringGetAsync(key);
            if (!value.IsNull)
                return JsonConvert.DeserializeObject<T>(value);
            else
                return default;
        }
        public List<RedisKey> GetKeys(string keyPattern)
        {
            return _redisServer.Keys(pattern: "*" + keyPattern + "*").ToList();
        }
        public bool IsInCache(string key)
        {
            return _redisCache.KeyExists(key);
        }
        public async Task<bool> Remove(string key)
        {
            return await _redisCache.KeyDeleteAsync(key);
        }
    }
}
