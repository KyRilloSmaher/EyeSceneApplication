using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class CelebrityConfiguration : IEntityTypeConfiguration<Celebrity>
    {
        public void Configure(EntityTypeBuilder<Celebrity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.MiddleName).HasMaxLength(100);
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Bio).HasMaxLength(1000);
            builder.Property(c => c.BirthDate).IsRequired();
            builder.Property(c => c.Nationality).IsRequired();
            builder.Property(c => c.Sex).IsRequired();
            builder.HasMany(c => c.Awards)
                   .WithOne(ca => ca.Celebrity)
                   .HasForeignKey(ca => ca.Celebirtyid);
            builder.HasMany(c => c.Images)
                   .WithOne(i => i.Celebrity)
                   .HasForeignKey(i => i.CelebirtyId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
} 