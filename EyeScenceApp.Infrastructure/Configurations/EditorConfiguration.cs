using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class EditorConfiguration : IEntityTypeConfiguration<Editor>
    {
        public void Configure(EntityTypeBuilder<Editor> builder)
        {
       
            builder.Property(e => e.EditedProjectsCount).IsRequired();
            builder.Property(e => e.EditingTechniques).IsRequired();
        }
    }
} 