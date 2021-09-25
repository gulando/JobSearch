using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JobSearch.Infrastructure.Context
{
    public class JobSearchContextFactory : IDesignTimeDbContextFactory<JobSearchContext>
    {
        public JobSearchContextFactory()
        {
            
        }
        
        public JobSearchContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<JobSearchContext>();

            if (builder == null)
            {
                throw new NullReferenceException(nameof(builder));
            }

            builder.UseNpgsql("Server=localhost; Database=JobSearch; User Id=postgres; Password=sqlAdmin2020!");

            return new JobSearchContext(builder.Options);
        }
    }
}