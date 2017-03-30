namespace zh.fang.csharp
{
    using System;

    /// <summary>
    /// GUID 扩展处理
    /// </summary>
    public static class GuidHelper
    {
        /// <summary>
        /// 生成新的 GUID 对象，并返回新对象的 16 位长度格式的字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidToString16(this Guid guid)
        {
            long i = 1;
            foreach (byte b in guid.ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 生成新的 GUID 对象，并返回新对象的字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidToString(this Guid guid)
        {
            return guid.ToString("N");
        }
    }
}
