using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
   
            builder.Property(p => p.ProducedProjectsCount).IsRequired();
            builder.Property(p => p.TotalBoxOfficeRevenue).IsRequired();
        }
    }
} 