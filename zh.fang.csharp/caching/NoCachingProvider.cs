namespace zh.fang.csharp.caching
{
    using System;

    public class NoCachingProvider : ICachingProvider
    {
        void ICachingProvider.Clear(string key)
        {
            
        }

        bool ICachingProvider.Exists(string key)
        {
            return false;
        }

        int ICachingProvider.Increment(string key, int defaultValue, int incrementValue)
        {
            return defaultValue;
        }

        void ICachingProvider.Set<TValue>(string key, TValue value, int? timeoutInSeconds)
        {
            
        }

        bool ICachingProvider.TryGet<TValue>(string key, out TValue value)
        {
            value = default(TValue);
            return false;
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}
