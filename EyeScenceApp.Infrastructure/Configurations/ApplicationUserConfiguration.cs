using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Nationality).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Age).IsRequired();
            builder.Property(u => u.code).HasMaxLength(256);
            builder.HasMany(u => u.Rates)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId);
            builder.HasMany(u => u.Favorites)
                   .WithOne(f => f.User)
                   .HasForeignKey(f => f.UserId);
            builder.HasMany(u => u.WatchLists)
                   .WithOne(w => w.User)
                   .HasForeignKey(w => w.UserId);
        }
    }
} 