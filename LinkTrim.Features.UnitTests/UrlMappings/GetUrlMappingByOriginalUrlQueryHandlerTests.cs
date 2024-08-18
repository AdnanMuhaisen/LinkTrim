using LinkTrim.Api.Core.Entities.UrlMappingAggregates.Errors;
using LinkTrim.Api.Dtos;
using LinkTrim.Api.Features.UrlMappings;
using LinkTrim.Features.UnitTests.TestingDbSetup;

namespace LinkTrim.Features.UnitTests.UrlMappings;

[Collection("LinkTrimDbCollection")]
public class GetUrlMappingByOriginalUrlQueryHandlerTests
{
    public LinkTrimTestDbFixture LinkTrimTestDbFixture { get; set; }

    public GetUrlMappingByOriginalUrlQueryHandlerTests(LinkTrimTestDbFixture linkTrimTestDbFixture)
        => (LinkTrimTestDbFixture) = (linkTrimTestDbFixture);

    [Fact]
    public async Task QueryHandler_GetAvailableUrlMapping_ReturnsExpectedUrlMapping()
    {
        // Arrange
        using var appDbContext = LinkTrimTestDbFixture.CreateContext();
        var originalUrl = "https://devblogs.microsoft.com/dotnet/?c=34&WT.mc_id=dotnet-35129-website";
        var query = new GetUrlMappingByOriginalUrl.Query(originalUrl);
        var handler = new GetUrlMappingByOriginalUrl.Handler(appDbContext);

        // Act
        var actual = await handler.Handle(query, default);

        // Assert
        Assert.NotEqual(UrlMappingErrors.NotFound, actual);
    }

    [Fact]
    public async Task QueryHandler_GetUnavailableUrlMapping_ReturnsNotFoundError()
    {
        // Arrange
        using var appDbContext = LinkTrimTestDbFixture.CreateContext();
        var originalUrl = "https://not-found.com";
        var query = new GetUrlMappingByOriginalUrl.Query(originalUrl);
        var handler = new GetUrlMappingByOriginalUrl.Handler(appDbContext);

        // Act
        var actual = await handler.Handle(query, default);

        // Assert
        Assert.Equal(UrlMappingErrors.NotFound, actual.FirstError);
    }
}