namespace zh.fang.csharp.caching.redis
{
    using System;
    using Newtonsoft.Json;
    using StackExchange.Redis;

    public static class StackExchangeRedisExtensions
    {
        public static TValue Get<TValue>(this IDatabase cache, string key)
        {
            return Deserialize<TValue>(cache.StringGet(key));
        }

        public static object Get(this IDatabase cache, string key)
        {
            return Deserialize<object>(cache.StringGet(key));
        }

        public static void Set(this IDatabase cache, string key, object value, int? timeoutInSeconds = null)
        {
            TimeSpan? expiry = null;
            if (timeoutInSeconds.HasValue)
            {
                expiry = new TimeSpan(0, 0, timeoutInSeconds.Value);
            }

            cache.StringSet(key, Serialize(value), expiry);
        }

        private static string Serialize(object value)
        {
            if (null == value)
            {
                return null;
            }

            return JsonConvert.SerializeObject(value);
        }

        private static TValue Deserialize<TValue>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(TValue);
            }
            return JsonConvert.DeserializeObject<TValue>(value);
        }
    }
}
