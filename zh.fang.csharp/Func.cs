namespace zh.fang.csharp
{
    using System;
    using System.Threading.Tasks;

    public static class FuncHelper
    {
        public static TResult Retry<TParam, TResult>(this Func<TParam, TResult> func, TParam parameter, Action<Exception> warn, int retryCount, int delayMilliseconds = 0)
        {
            int retry = 0;
            TResult result = default(TResult);
            while (retryCount > retry)
            {
                retry++;
                try
                {
                    result = func.Invoke(parameter);
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= retry)
                    {
                        warn.Invoke(ex);
                    }
                }

                if (delayMilliseconds > 0)
                {
                    Task.Delay(delayMilliseconds).Wait();
                }
            }

            return result;
        }
    }
}
