using JobSearch.ApplicationCore.Common.Abstractions.DataSeed;
using JobSearch.ApplicationCore.Common.Abstractions.Repositories;
using JobSearch.Infrastructure.Context;
using JobSearch.Infrastructure.DataSeed;
using JobSearch.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.Infrastructure.IoC.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobSearchContext>(options =>
                options.UseNpgsql("Server=localhost; Database=JobSearch; User Id=postgres; Password=sqlAdmin2020!"));
        }
        
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IJobRepository, JobRepository>()   
                .AddSingleton<IDataSeed, DataSeed.DataSeed>();
        }
    }
}