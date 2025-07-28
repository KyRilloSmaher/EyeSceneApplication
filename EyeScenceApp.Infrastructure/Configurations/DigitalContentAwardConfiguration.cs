using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class DigitalContentAwardConfiguration : IEntityTypeConfiguration<DigitalContentAward>
    {
        public void Configure(EntityTypeBuilder<DigitalContentAward> builder)
        {

            builder.Property(dca => dca.Name).IsRequired();
            builder.Property(dca => dca.AwardedDate).IsRequired();
            builder.Property(dca => dca.Category).IsRequired();
            builder.Property(dca => dca.Organization).IsRequired();
            builder.HasMany(dca => dca.DigitalContents)
                   .WithOne(d => d.Award)
                   .HasForeignKey(d => d.AwardId);
        }
    }
} 