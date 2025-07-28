using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class MovieCastConfiguration : IEntityTypeConfiguration<MovieCast>
    {
        public void Configure(EntityTypeBuilder<MovieCast> builder)
        {
            builder.HasKey(mc => mc.Id);
            builder.Property(mc => mc.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(mc => mc.RoleName).HasMaxLength(500).IsRequired();
            builder.HasOne(mc => mc.DigitalContent)
                   .WithMany(dc => dc.MovieCasts)
                   .HasForeignKey(mc => mc.DigitalContentId);
            builder.HasOne(mc => mc.Actor)
                   .WithMany(a => a.MovieCasts)
                   .HasForeignKey(mc => mc.ActorId);
        }
    }
} 