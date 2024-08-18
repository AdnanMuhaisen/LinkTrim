using LinkTrim.Api.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Features.UnitTests.TestingDbSetup;

public class LinkTrimTestDbFixture
{
    private const string ConnectionString = @"Server=localhost; Database=LinkTrim_TestingEnv; Integrated Security=SSPI; TrustServerCertificate=True";
    private static bool _databaseInitialized;
    private static object _lock = new();

    public LinkTrimTestDbFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var appDbContext = CreateContext();

                appDbContext.Database.EnsureDeleted();
                appDbContext.Database.EnsureCreated();

                appDbContext.UrlMappings.AddRange(SeedData.GetUrlMappings());
                appDbContext.SaveChanges();
            }

            _databaseInitialized = true;
        }
    }

    public AppDbContext CreateContext()
    {
        var dbContextOption = new DbContextOptionsBuilder<AppDbContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSqlServer(ConnectionString)
            .Options;

        return new(dbContextOption);
    }
}