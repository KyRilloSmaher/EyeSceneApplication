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
    public class CelebirtyImageConfiguration : IEntityTypeConfiguration<CelebirtyImages>
    {
        public void Configure(EntityTypeBuilder<CelebirtyImages> builder)
        {
           builder.HasKey(builder => new { builder.ImageId, builder.CelebirtyId });

            builder.Property(builder => builder.ImageId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(builder => builder.CelebirtyId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasOne(builder => builder.Celebrity)
                .WithMany(celebrity => celebrity.Images)
                .HasForeignKey(builder => builder.CelebirtyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(builder => builder.Image)
                .WithMany(image => image.CelebirtyImages)
                .HasForeignKey(builder => builder.ImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
