using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class WorksOnConfiguration : IEntityTypeConfiguration<WorksOn>
    {
        public void Configure(EntityTypeBuilder<WorksOn> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id)
                   .ValueGeneratedOnAdd();
            builder.HasOne(w => w.DigitalContent)
                   .WithMany(dc => dc.WorksOn)
                   .HasForeignKey(w => w.DigitalContentId);
            builder.HasOne(w => w.Crew)
                   .WithMany(c => c.WorksOn)
                   .HasForeignKey(w => w.CrewId);
        }
    }
} 