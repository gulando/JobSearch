using System.Text.Json.Serialization;
using System.Threading;
using JobSearch.Api.Middlewares;
using JobSearch.ApplicationCore.Common.Abstractions.DataSeed;
using JobSearch.Infrastructure.IoC.ApplicationCore;
using JobSearch.Infrastructure.IoC.Infrastructure;
using JobSearch.Infrastructure.IoC.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace JobSearch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMediatR();
            services.AddConfigurations(Configuration);
            services.AddDatabase(Configuration);
            services.RegisterRepositories();

            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "JobSearch.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobSearch.Api v1"));
            
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.ApplicationServices.GetRequiredService<IDataSeed>().SeedAllInitialDataAsync(CancellationToken.None);
        }
    }
}