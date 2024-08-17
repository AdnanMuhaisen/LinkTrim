using LinkTrim.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Api.Infrastructure.Data.Contexts;

public class AppDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<UrlMapping> UrlMappings => Set<UrlMapping>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        #region FluentApiConfigurations
        modelBuilder.Entity<UrlMapping>()
            .HasIndex(c => c.OriginalUrl);

        modelBuilder.Entity<UrlMapping>()
            .HasQueryFilter(c => !c.IsDeleted);
        #endregion
    }
}