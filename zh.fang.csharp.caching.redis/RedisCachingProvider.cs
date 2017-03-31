namespace zh.fang.csharp.caching.redis
{
    using System;
    using StackExchange.Redis;

    public class RedisCachingProvider : ICachingProvider
    {
        public RedisCachingProvider(string host, bool ssl = true)
        {
            AssertHost(host);
            var config = new ConfigurationOptions
            {
                EndPoints = {
                    { host }
                },
                Ssl = ssl
            };
            Initialize(config);
        }

        public RedisCachingProvider(string host, int port, bool ssl = true)
        {
            AssertHost(host);
            var config = new ConfigurationOptions
            {
                EndPoints = {
                    { host, port }
                },
                Ssl = ssl
            };
            Initialize(config);
        }

        public RedisCachingProvider(string host, int port, string password, bool ssl = true)
        {
            AssertHost(host);
            var config = new ConfigurationOptions {
                EndPoints = {
                    { host, port }
                },
                Password = password,
                Ssl = ssl
            };
            Initialize(config);
        }

        private void AssertHost(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentNullException(nameof(host));
            }
        }

        public RedisCachingProvider(ConfigurationOptions configOptions)
        {
            if (null == configOptions)
            {
                throw new ArgumentNullException(nameof(configOptions));
            }

            Initialize(configOptions);
        }

        private void Initialize(ConfigurationOptions configOptions)
        {
            RedisConnector.SetConnection(configOptions);
            Redis = RedisConnector.Connection.GetDatabase();
        }

        void ICachingProvider.Clear(string key)
        {
            AssertKey(key);
            Redis.KeyDelete(key);
        }

        bool ICachingProvider.Exists(string key)
        {
            AssertKey(key);
            return Redis.KeyExists(key);
        }

        int ICachingProvider.Increment(string key, int defaultValue, int incrementValue)
        {
            AssertKey(key);
            return Convert.ToInt32(Redis.StringIncrement(key, incrementValue));
        }

        void ICachingProvider.Set<TValue>(string key, TValue value, int? timeoutInSeconds)
        {
            AssertKey(key);
            Redis.Set(key, value, timeoutInSeconds);
        }

        bool ICachingProvider.TryGet<TValue>(string key, out TValue value)
        {
            AssertKey(key);
            try
            {
                value = Redis.Get<TValue>(key);
                return !Equals(value, default(TValue));
            }
            catch
            {
                value = default(TValue);
                return false;
            }
        }

        private void AssertKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        public void Dispose()
        {
            
        }

        public IDatabase Redis { get; private set; }
    }
}
