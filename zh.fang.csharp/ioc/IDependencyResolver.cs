namespace zh.fang.csharp.ioc
{
    using System;

    public interface IDependencyResolver
    {
        TInstance Resolve<TInstance>();

        object Resolve(Type type);
    }
}
