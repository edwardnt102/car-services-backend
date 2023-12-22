namespace Utility.Redis.StackExchangeRedis.Interfaces
{
    public interface IRedisPubSubProvider
    {
        void Publish(string channel, string message);
        void SubScribe(string channel);
    }
}
