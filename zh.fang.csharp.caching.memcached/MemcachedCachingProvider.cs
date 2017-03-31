namespace zh.fang.csharp.caching.memcached
{
    using System;
    using Enyim.Caching;
    using Enyim.Caching.Configuration;
    using Enyim.Caching.Memcached;

    public class MemcachedCachingProvider : ICachingProvider
    {
        public MemcachedCachingProvider(string configSectionName)
        {
            if (string.IsNullOrWhiteSpace(configSectionName))
            {
                throw new ArgumentNullException(nameof(configSectionName));
            }
            Client = new MemcachedClient(configSectionName);
        }

        public MemcachedCachingProvider(IMemcachedClientConfiguration configuration)
        {
            if (null == configuration)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            Client = new MemcachedClient(configuration);
        }

        public MemcachedCachingProvider(MemcachedClient memcachedClient)
        {
            Client = memcachedClient ?? throw new ArgumentNullException(nameof(memcachedClient));
        }

        public MemcachedCachingProvider(string host, int port, string username = null, string passwd = null, Type authenticationType = null)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            var config = new MemcachedClientConfiguration { Protocol = MemcachedProtocol.Binary };
            config.AddServer(host, port);

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(passwd))
            {
                config.Authentication.Parameters["userName"] = username;
                config.Authentication.Parameters["password"] = passwd;
                config.Authentication.Type = authenticationType ?? typeof(PlainTextAuthenticator);
            }
            Client = new MemcachedClient(config);
        }

        void ICachingProvider.Clear(string key)
        {
            AssertKey(key);
            Client.Remove(key);
        }

        bool ICachingProvider.Exists(string key)
        {
            return ((ICachingProvider)this).TryGet<object>(key, out object value);
        }

        int ICachingProvider.Increment(string key, int defaultValue, int incrementValue)
        {
            AssertKey(key);
            var incre = Client.Increment(key, Convert.ToUInt64(defaultValue), Convert.ToUInt64(incrementValue));
            return Convert.ToInt32(incre);
        }

        void ICachingProvider.Set<TValue>(string key, TValue value, int? timeoutInSeconds)
        {
            AssertKey(key);
            if (timeoutInSeconds.HasValue)
            {
                Client.Store(StoreMode.Set, key, value, new TimeSpan(0, 0, timeoutInSeconds.Value));
            }

            if (!timeoutInSeconds.HasValue)
            {
                Client.Store(StoreMode.Set, key, value);
            }
        }

        bool ICachingProvider.TryGet<TValue>(string key, out TValue value)
        {
            AssertKey(key);
            try
            {
                value = Client.Get<TValue>(key);
                return !Equals(value, default(TValue));
            }
            catch
            {
                value = default(TValue);
                return false;
            }
        }
        
        void IDisposable.Dispose()
        {
            Client.Dispose();
            Client = null;
        }

        private void AssertKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(key);
            }
        }

        public MemcachedClient Client { get; private set; }
    }
}
