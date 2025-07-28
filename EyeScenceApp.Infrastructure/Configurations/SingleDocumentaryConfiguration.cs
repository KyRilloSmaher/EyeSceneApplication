using EyeScenceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeScenceApp.Infrastructure.Configurations
{
    public class SingleDocumentaryConfiguration : IEntityTypeConfiguration<SingleDocumentary>
    {
        public void Configure(EntityTypeBuilder<SingleDocumentary> builder)
        {
            
        }
    }
} 