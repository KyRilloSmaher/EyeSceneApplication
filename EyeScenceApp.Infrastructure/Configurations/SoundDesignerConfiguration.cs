using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class SoundDesignerConfiguration : IEntityTypeConfiguration<SoundDesigner>
    {
        public void Configure(EntityTypeBuilder<SoundDesigner> builder)
        {
   
            builder.Property(sd => sd.KnownForSoundtracks).HasMaxLength(500);
        }
    }
} 