using System;
using System.Linq;
using JobSearch.Infrastructure.DataSeed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.Infrastructure.IoC.WebApi
{
    /// <summary>
    /// Inject config models.
    /// </summary>
    public static class ConfigurationModule
    {
        /// <summary>
        /// Add config models.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>The original services object.</returns>
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.Contains(nameof(JobSearch), StringComparison.InvariantCultureIgnoreCase));

            services.AddAutoMapper(
                cfg => { cfg.AllowNullCollections = true; },
                assemblies,
                ServiceLifetime.Scoped);
            
            return services
                .Configure<DataSeedOptions>(configuration.GetSection($"{ConfigurationConstants.App}:{ConfigurationConstants.Seed}"));
        }
    }
}