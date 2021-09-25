using System.Reflection;
using System.Threading;
using JobSearch.ApplicationCore;
using JobSearch.ApplicationCore.Common.Abstractions.DataSeed;
using JobSearch.Infrastructure.IoC.ApplicationCore;
using JobSearch.Infrastructure.IoC.Infrastructure;
using JobSearch.Infrastructure.IoC.WebApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JobSearch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMediatR();
            services.AddConfigurations(Configuration);
            services.AddDatabase(Configuration);
            services.RegisterRepositories();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "JobSearch.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobSearch.Api v1"));

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.ApplicationServices.GetRequiredService<IDataSeed>().SeedAllInitialDataAsync(CancellationToken.None);
        }
    }
}