using JobSearch.Domain.Entities;
using JobSearch.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Infrastructure.Context
{
    public class JobSearchContext : DbContext
    {
        public JobSearchContext()
        {
            
        }

        public JobSearchContext(DbContextOptions<JobSearchContext> options)
            : base(options)
        {
            
        }
        
        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new EmploymentTypeConfiguration());
            
            base.OnModelCreating(builder);
        }
    }
}