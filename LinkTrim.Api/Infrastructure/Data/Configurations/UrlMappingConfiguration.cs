using LinkTrim.Api.Core.Entities.UrlMappingAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkTrim.Api.Infrastructure.Data.Configurations;

public class UrlMappingConfiguration : IEntityTypeConfiguration<UrlMapping>
{
    public void Configure(EntityTypeBuilder<UrlMapping> builder)
    {
        builder.ToTable("UrlMappings")
            .HasKey(k => k.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.OriginalUrl)
            .HasMaxLength(4_000);

        builder.Property(x => x.ShortenedUrl)
            .HasMaxLength(50);     
        
        builder.Property(x => x.ShortCode)
            .HasMaxLength(20);

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);            
    }
}