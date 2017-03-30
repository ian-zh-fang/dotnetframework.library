namespace zh.fang.csharp.caching
{
    using System;

    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICachingProvider : IDisposable
    {
        /// <summary>
        /// 设置缓存中指定 key 指向的数据对象，如果指定的 key 不存在，那么向缓存中追加当前 key ，并设置当前 key 指向的数据对象
        /// </summary>
        /// <typeparam name="TValue">数据对象类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">缓存中 key 指向的数据对象</param>
        /// <param name="timeoutInSeconds">过期时间，单位：秒</param>
        void Set<TValue>(string key, TValue value, int? timeoutInSeconds = null);

        /// <summary>
        /// 尝试获取缓存中指定 key 指向的数据对象，返回尝试的状态，成功返回 true，否则，返回 false。
        /// </summary>
        /// <typeparam name="TValue">数据对象类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">缓存中 key 指向的数据对象</param>
        /// <returns></returns>
        bool TryGet<TValue>(string key, out TValue value);

        /// <summary>
        /// 删除缓存中的 key 对象和指向的数据对象
        /// </summary>
        /// <param name="key"></param>
        void Clear(string key);

        /// <summary>
        /// 缓存中是否存在指定的 key 对象，存在返回 true。否则，返回 false
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 增量计算
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        int Increment(string key, int defaultValue, int incrementValue);
    }
}
