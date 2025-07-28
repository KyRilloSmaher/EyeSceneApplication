using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class AwardConfiguration : IEntityTypeConfiguration<Award>
    {
        public void Configure(EntityTypeBuilder<Award> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.PosterId).IsRequired();
            builder.Property(a => a.AwardedDate).IsRequired();
            builder.Property(a => a.Category).IsRequired();
            builder.Property(a => a.Organization).IsRequired();
            builder.HasOne(a => a.Image)
                   .WithOne(i=>i.award)
                   .HasForeignKey<Award>(a => a.PosterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 