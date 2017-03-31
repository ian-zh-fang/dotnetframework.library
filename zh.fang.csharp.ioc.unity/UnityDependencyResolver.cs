namespace zh.fang.csharp.ioc.unity
{
    using System;
    using Microsoft.Practices.Unity;

    public class UnityDependencyResolver : DependencyResolver
    {
        public UnityDependencyResolver(IUnityContainer container)
        {
            Container = container;
        }

        protected override TInstance Instacne<TInstance>()
        {
            return Container.Resolve<TInstance>();
        }

        protected override object Instance(Type type)
        {
            return Container.Resolve(type);
        }

        public IUnityContainer Container { get; private set; }
    }
}
