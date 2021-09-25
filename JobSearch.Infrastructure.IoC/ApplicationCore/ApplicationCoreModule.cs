using Autofac;
using MediatR;

namespace JobSearch.Infrastructure.IoC.ApplicationCore
{
    public static class ApplicationCoreModule
    {
        public static void AddMediatR(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            
            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            }).InstancePerLifetimeScope();
        }
    }
}