using JobSearch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearch.Infrastructure.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("jobs");
            
            builder.Property(b => b.Title).IsRequired();
            
            builder.HasOne(t => t.Company)
                .WithMany(m => m.Jobs)
                .HasForeignKey(bc => bc.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(t => t.Location)
                .WithMany(m => m.Jobs)
                .HasForeignKey(bc => bc.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(t => t.Category)
                .WithMany(m => m.Jobs)
                .HasForeignKey(bc => bc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(t => t.EmploymentType)
                .WithMany(m => m.Jobs)
                .HasForeignKey(bc => bc.EmploymentTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}