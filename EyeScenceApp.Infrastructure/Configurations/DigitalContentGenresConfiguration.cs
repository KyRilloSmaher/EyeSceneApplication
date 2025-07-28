using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class DigitalContentGenresConfiguration : IEntityTypeConfiguration<DigitalContentGenres>
    {
        public void Configure(EntityTypeBuilder<DigitalContentGenres> builder)
        {
            builder.HasKey(dcg => new { dcg.GenreId, dcg.DigitalContentId });
            builder.Property(dcg => dcg.CreatedAt).IsRequired();
            builder.Property(dcg => dcg.UpdatedAt).IsRequired();
            builder.HasOne(dcg => dcg.Genre)
                   .WithMany(g => g.contents)
                   .HasForeignKey(dcg => dcg.GenreId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(dcg => dcg.DigitalContent)
                   .WithMany(dc => dc.Genres)
                   .HasForeignKey(dcg => dcg.DigitalContentId);
        }
    }
} 