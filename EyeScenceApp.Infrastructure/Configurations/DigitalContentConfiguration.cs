using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class DigitalContentConfiguration : IEntityTypeConfiguration<DigitalContent>
    {
        public void Configure(EntityTypeBuilder<DigitalContent> builder)
        {
            builder.HasKey(dc => dc.Id);

            builder.Property(dc => dc.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(dc => dc.Title)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(dc => dc.ShortDescription)
                   .HasMaxLength(2500)
                   .IsRequired();

            builder.Property(dc => dc.CountryOfOrigin).IsRequired();
            builder.Property(dc => dc.DurationByMinutes).IsRequired();
            builder.Property(dc => dc.ReleaseYear).IsRequired();
            builder.Property(dc => dc.PosterId)
                   .IsRequired()
                   .HasColumnName("PosterId");

            builder.Property(dc => dc.Rate).IsRequired();
            builder.Property(dc => dc.UploadingDate).IsRequired();

            builder.HasMany(dc => dc.Ratings)
                   .WithOne(r => r.DigitalContent)
                   .HasForeignKey(r => r.DigitalContentId);

            builder.HasMany(dc => dc.Genres)
                   .WithOne(g => g.DigitalContent)
                   .HasForeignKey(g => g.DigitalContentId);

            builder.HasMany(dc => dc.FavoriteMovies)
                   .WithOne(f => f.DigitalContent)
                   .HasForeignKey(f => f.DigitalContentId);

            builder.HasMany(dc => dc.WatchListMovies)
                   .WithOne(w => w.DigitalContent)
                   .HasForeignKey(w => w.DigitalContentId);

            builder.HasMany(dc => dc.MovieCasts)
                   .WithOne(mc => mc.DigitalContent)
                   .HasForeignKey(mc => mc.DigitalContentId);

            builder.HasMany(dc => dc.WorksOn)
                   .WithOne(w => w.DigitalContent)
                   .HasForeignKey(w => w.DigitalContentId);

            builder.HasOne(dc => dc.Image)
                   .WithOne(i => i.DigitalContent)
                   .HasForeignKey<DigitalContent>(dc => dc.PosterId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }

    }
} 