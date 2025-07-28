using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
           
            builder.Property(d => d.DirectedMoviesCount).IsRequired();
            builder.Property(d => d.VisionStatement).HasMaxLength(500);
        }
    }
} 