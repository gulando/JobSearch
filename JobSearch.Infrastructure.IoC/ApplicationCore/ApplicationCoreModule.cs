using JobSearch.ApplicationCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.Infrastructure.IoC.ApplicationCore
{
    public static class ApplicationCoreModule
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(MediatREntryPoint).Assembly);

        }
    }
}