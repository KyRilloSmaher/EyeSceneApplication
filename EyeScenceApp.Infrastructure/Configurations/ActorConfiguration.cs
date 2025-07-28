using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(a => a.TotalMovies).IsRequired();
            builder.Property(a => a.IsCurrentlyActive).IsRequired();
            builder.Property(a => a.ActingStyle).IsRequired();
            builder.HasMany(a => a.MovieCasts)
                   .WithOne(mc => mc.Actor)
                   .HasForeignKey(mc => mc.ActorId);
        }
    }
} 