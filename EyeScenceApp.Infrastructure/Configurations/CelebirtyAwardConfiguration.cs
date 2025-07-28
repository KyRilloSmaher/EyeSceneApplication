using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class CelebirtyAwardConfiguration : IEntityTypeConfiguration<CelebirtyAward>
    {
        public void Configure(EntityTypeBuilder<CelebirtyAward> builder)
        {
           
            builder.Property(ca => ca.Name).IsRequired();
            builder.Property(ca => ca.AwardedDate).IsRequired();
            builder.Property(ca => ca.Category).IsRequired();
            builder.Property(ca => ca.Organization).IsRequired();
        }
    }
} 