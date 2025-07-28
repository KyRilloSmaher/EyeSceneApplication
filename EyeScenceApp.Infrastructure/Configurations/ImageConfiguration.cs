using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {

        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(c => c.Url).HasColumnType("nvarchar(500)").IsRequired();
            builder.HasOne(c => c.award)
                   .WithOne(i => i.Image)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.DigitalContent)
                  .WithOne(i => i.Image)
                  .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.genre)
              .WithOne(i => i.Image)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
