using JobSearch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearch.Infrastructure.Configurations
{
    public class EmploymentTypeConfiguration : IEntityTypeConfiguration<EmploymentType>
    {
        public void Configure(EntityTypeBuilder<EmploymentType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(EmploymentType));

            builder.Property(b => b.Type).IsRequired();
        }
    }
}