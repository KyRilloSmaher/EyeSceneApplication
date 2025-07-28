using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id)
                   .ValueGeneratedOnAdd();
            builder.Property(g => g.Name).HasMaxLength(255).IsRequired();
            builder.Property(g => g.PosterId).IsRequired();
            builder.HasMany(g => g.contents)
                   .WithOne(dcg => dcg.Genre)
                   .HasForeignKey(dcg => dcg.GenreId);
            builder.HasOne(g=>g.Image)
                   .WithOne(im=> im.genre)
                    .HasForeignKey<Genre>(g=>g.PosterId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 