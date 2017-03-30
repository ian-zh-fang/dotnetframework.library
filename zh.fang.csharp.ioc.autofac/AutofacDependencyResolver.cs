namespace zh.fang.csharp.ioc.autofac
{
    using System;
    using Autofac;

    public class AutofacDependencyResolver : DependencyResolver
    {
        public IContainer Container { get; private set; }

        public AutofacDependencyResolver(IContainer container)
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
    }
}
