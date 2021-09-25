using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JobSearch.Domain.Entities;
using JobSearch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JobSearch.Infrastructure.DataSeed
{
    public class DataSeed : IDataSeed, IDisposable
    {
        protected JobSearchContext JobSearchContext { get; set; }

        protected string DataSeedJsonFilesRootPath { get; set; }

        private readonly IServiceScope _serviceScope;

        public DataSeed(IServiceProvider serviceProvider, IOptions<DataSeedOptions> dataSeedOptions)
        {
            _serviceScope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
            JobSearchContext = _serviceScope.ServiceProvider.GetRequiredService<JobSearchContext>();
            
            DataSeedJsonFilesRootPath = dataSeedOptions.Value.JsonFilesRootPath;
        }
        
        public void Dispose()
        {
            JobSearchContext.Dispose();
        }
        
        public async Task SeedAllInitialDataAsync(CancellationToken cancellationToken = default)
        {
            var pendingMigrations = (await JobSearchContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();
            var totalMigrationsCount = JobSearchContext.Database.GetMigrations()?.Count();

            if (totalMigrationsCount == pendingMigrations.Count && pendingMigrations.Any())
            {
                await JobSearchContext.Database.EnsureDeletedAsync(cancellationToken);
                await JobSearchContext.Database.MigrateAsync(cancellationToken);
            }
            
            await SeedDatabaseAsync(cancellationToken);
        }

        private async Task SeedDatabaseAsync(CancellationToken cancellationToken)
        {
            var companies = GetCompanies();
            var locations = GetLocations();
            var  categories= GetCategories();
            var employmentTypes = GetEmploymentTypes();
            var jobs = GetJobs();


            if (await JobSearchContext.Jobs.AsNoTracking().CountAsync(cancellationToken) != jobs.Count)
            {
                var newCompanies = companies.Where(x =>
                    !JobSearchContext.Companies.AsNoTracking()
                        .Any(e => e.Id == x.Id)).ToList();

                if (newCompanies.Any())
                {
                    await JobSearchContext.Companies.AddRangeAsync(newCompanies, cancellationToken);
                }
                
                var newLocations = locations.Where(x =>
                    !JobSearchContext.Locations.AsNoTracking()
                        .Any(e => e.Id == x.Id)).ToList();

                if (newLocations.Any())
                {
                    await JobSearchContext.Locations.AddRangeAsync(newLocations, cancellationToken);
                }
                
                var newCategories = categories.Where(x =>
                    !JobSearchContext.Categories.AsNoTracking()
                        .Any(e => e.Id == x.Id)).ToList();

                if (newCategories.Any())
                {
                    await JobSearchContext.Categories.AddRangeAsync(newCategories, cancellationToken);
                }
                
                var newEmploymentTypes = employmentTypes.Where(x =>
                    !JobSearchContext.EmploymentTypes.AsNoTracking()
                        .Any(e => e.Id == x.Id)).ToList();

                if (newEmploymentTypes.Any())
                {
                    await JobSearchContext.EmploymentTypes.AddRangeAsync(newEmploymentTypes, cancellationToken);
                }
                
                var newJobs = jobs.Where(x =>
                    !JobSearchContext.Jobs.AsNoTracking()
                        .Any(e => e.Title == x.Title)).ToList();

                if (newJobs.Any())
                {
                    await JobSearchContext.Jobs.AddRangeAsync(newJobs, cancellationToken);
                }
            }

            if (JobSearchContext.ChangeTracker.HasChanges())
            {
                await JobSearchContext.SaveChangesAsync(cancellationToken);
            }
        }

        private List<Company> GetCompanies()
        {
            return ReadSeedDataFromJsonFile<Company>(DataSeedJsonFilesRootPath, nameof(Company));
        }
        
        private List<Location> GetLocations()
        {
            return ReadSeedDataFromJsonFile<Location>(DataSeedJsonFilesRootPath, nameof(Location));
        }
        
        private List<Category> GetCategories()
        {
            return ReadSeedDataFromJsonFile<Category>(DataSeedJsonFilesRootPath, nameof(Category));
        }
        
        private List<EmploymentType> GetEmploymentTypes()
        {
            return ReadSeedDataFromJsonFile<EmploymentType>(DataSeedJsonFilesRootPath, nameof(EmploymentType));
        }
        
        private List<Job> GetJobs()
        {
            return ReadSeedDataFromJsonFile<Job>(DataSeedJsonFilesRootPath, nameof(Job));
        }
        
        private static List<T> ReadSeedDataFromJsonFile<T>(string path, string fileName)
        {
            var fullPath = Path.Combine(path, $"{fileName}.json");
            var data = File.ReadAllText(fullPath, Encoding.UTF8);

            return JsonSerializer.Deserialize<List<T>>(data);
        }
    }
}