namespace zh.fang.csharp.caching.redis
{
    using StackExchange.Redis;

    public static class RedisConnector
    {
        public static ConnectionMultiplexer Connection { get; private set; }

        public static void SetConnection(ConfigurationOptions configOptions)
        {
            if (Connection == null)
            {
                configOptions.AbortOnConnectFail = false;
                Connection = ConnectionMultiplexer.Connect(configOptions);
            }
        }
    }
}
