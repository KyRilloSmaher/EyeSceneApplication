using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class DigitalContentAwardsConfiguration : IEntityTypeConfiguration<DigitalContentAwards>
    {
        public void Configure(EntityTypeBuilder<DigitalContentAwards> builder)
        {
            builder.HasKey(dca => new { dca.AwardId, dca.DigitalContentId });

            builder.Property(dca => dca.Year).IsRequired();

            builder.HasOne(dca => dca.DigitalContent)
                   .WithMany(dc => dc.Awards)
                   .HasForeignKey(dca => dca.DigitalContentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dca => dca.Award)
                   .WithMany(a => a.DigitalContents)
                   .HasForeignKey(dca => dca.AwardId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
} 