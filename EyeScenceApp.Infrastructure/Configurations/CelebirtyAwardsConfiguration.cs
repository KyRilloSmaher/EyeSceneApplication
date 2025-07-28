using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class CelebirtyAwardsConfiguration : IEntityTypeConfiguration<CelebirtyAwards>
    {
        public void Configure(EntityTypeBuilder<CelebirtyAwards> builder)
        {
            builder.HasKey(ca => new { ca.AwardId, ca.Celebirtyid });
            builder.Property(ca => ca.Year).IsRequired();
            builder.HasOne(ca => ca.Celebrity)
                   .WithMany(c => c.Awards)
                   .HasForeignKey(ca => ca.Celebirtyid);
            builder.HasOne(ca => ca.Award)
                   .WithMany(a => a.celebirties)
                   .HasForeignKey(ca => ca.AwardId);
        }
    }
} 