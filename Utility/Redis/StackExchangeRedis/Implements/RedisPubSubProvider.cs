using StackExchange.Redis;
using System;
using Utility.Redis.StackExchangeRedis.Interfaces;

namespace Utility.Redis.StackExchangeRedis.Implements
{
    public class RedisPubSubProvider : IRedisPubSubProvider
    {
        ISubscriber publish;
        ISubscriber subscriber;
        IDatabase redis;

        public RedisPubSubProvider()
        {
            redis = RedisStore.RedisCache;
            publish = redis.Multiplexer.GetSubscriber();
            subscriber = redis.Multiplexer.GetSubscriber();
        }

        public void Publish(string channel, string message)
        {
            try
            {
                publish.Publish(channel, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SubScribe(string channel)
        {
            try
            {
                subscriber.Subscribe(channel, (ch, msg) =>
                {
                    //return func(msg);
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
