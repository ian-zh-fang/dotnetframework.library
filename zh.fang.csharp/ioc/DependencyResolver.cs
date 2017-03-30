namespace zh.fang.csharp.ioc
{
    using System;

    public abstract class DependencyResolver : IDependencyResolver
    {
        TInstance IDependencyResolver.Resolve<TInstance>()
        {
            try
            {
                return Instacne<TInstance>();
            }
            catch (Exception ex)
            {
                throw new DependencyResolverException(typeof(TInstance), this, ex);
            }
        }

        object IDependencyResolver.Resolve(Type type)
        {
            try
            {
                return Instance(type);
            }
            catch (Exception ex)
            {
                throw new DependencyResolverException(type, this, ex);
            }
        }

        protected abstract TInstance Instacne<TInstance>();

        protected abstract object Instance(Type type);
    }
}
