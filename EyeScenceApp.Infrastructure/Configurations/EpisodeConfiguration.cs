using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(e => e.posterId).IsRequired();
            builder.Property(e => e.Title).HasMaxLength(255).IsRequired();
            builder.Property(e => e.ShortDescription).HasMaxLength(2500).IsRequired();
            builder.Property(e => e.DurationByMinutes).IsRequired();
            builder.Property(e => e.Season).IsRequired();
            builder.Property(e => e.EpisodeNumber).IsRequired();
            builder.HasOne(e => e.Series)
                   .WithMany()
                   .HasForeignKey(e => e.SeriesId);

            builder.HasOne(e => e.poster)
                  .WithOne(i=>i.Episode)
                  .HasForeignKey<Episode>(e => e.posterId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
} 