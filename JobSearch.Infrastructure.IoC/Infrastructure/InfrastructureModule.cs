using JobSearch.Infrastructure.Context;
using JobSearch.Infrastructure.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.Infrastructure.IoC.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<JobSearchContext>(options =>
                options.UseNpgsql("Server=localhost; Database=JobSearch; User Id=postgres; Password=sqlAdmin2020!"));
        }
        
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<IDataSeed, DataSeed.DataSeed>();
        }
    }
}