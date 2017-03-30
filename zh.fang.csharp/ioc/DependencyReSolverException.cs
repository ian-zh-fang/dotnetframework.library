namespace zh.fang.csharp.ioc
{
    using System;

    public class DependencyResolverException : Exception
    {
        public Type DependencyType { get; private set; }

        public IDependencyResolver DependencyResolver { get; private set; }

        public DependencyResolverException(Type dependencyType, IDependencyResolver dependencyResolver, Exception innerException = null)
            :base($"can not resolve type {dependencyType}. make sure you have configured your ioc container for this type. View the InnerException for more details."
                 , innerException)
        {
            DependencyType = dependencyType;
            DependencyResolver = dependencyResolver;
        }
    }
}
