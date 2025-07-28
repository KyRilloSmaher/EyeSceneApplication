using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(r => r.Value).IsRequired();
            builder.Property(r => r.Review).HasMaxLength(1000);
            builder.Property(r => r.LikesCount).IsRequired();
            builder.Property(r => r.DislikesCount).IsRequired();
            builder.Property(r => r.Date).IsRequired();
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Rates)
                   .HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.DigitalContent)
                   .WithMany(dc => dc.Ratings)
                   .HasForeignKey(r => r.DigitalContentId);
        }
    }
} 