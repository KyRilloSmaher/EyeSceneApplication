using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.HasKey(w => new { w.UserId, w.DigitalContentId });
            builder.HasOne(w => w.User)
                   .WithMany(u => u.WatchLists)
                   .HasForeignKey(w => w.UserId);
            builder.HasOne(w => w.DigitalContent)
                   .WithMany(dc => dc.WatchListMovies)
                   .HasForeignKey(w => w.DigitalContentId);
        }
    }
} 