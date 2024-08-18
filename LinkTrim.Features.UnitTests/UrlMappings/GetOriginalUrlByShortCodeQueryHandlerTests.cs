using LinkTrim.Api.Features.UrlMappings;
using LinkTrim.Features.UnitTests.TestingDbSetup;

namespace LinkTrim.Features.UnitTests.UrlMappings;

[Collection("LinkTrimDbCollection")]
public class GetOriginalUrlByShortCodeQueryHandlerTests
{
    public LinkTrimTestDbFixture LinkTrimTestDbFixture { get; set; }

    public GetOriginalUrlByShortCodeQueryHandlerTests(LinkTrimTestDbFixture linkTrimTestDbFixture)
        => (LinkTrimTestDbFixture) = (linkTrimTestDbFixture);

    [Fact]
    public async Task QueryHandler_AvailableShortCode_ReturnsExpectedOriginalUrl()
    {
        // Arrange
        using var appDbContext = LinkTrimTestDbFixture.CreateContext();
        var shortCode = "70cb";
        var query = new GetOriginalUrlByShortCode.Query(shortCode);
        var handler = new GetOriginalUrlByShortCode.Handler(appDbContext);
        var expected = "https://devblogs.microsoft.com/dotnet/?c=34&WT.mc_id=dotnet-35129-website";

        // Act
        var actual = await handler.Handle(query, default);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task QueryHandler_UnavailableShortCode_ReturnsNotFoundPathSegment()
    {
        // Arrange
        using var appDbContext = LinkTrimTestDbFixture.CreateContext();
        var shortCode = "----";
        var query = new GetOriginalUrlByShortCode.Query(shortCode);
        var handler = new GetOriginalUrlByShortCode.Handler(appDbContext);
        var expected = "not-found";

        // Act
        var actual = await handler.Handle(query, default);

        // Assert
        Assert.Equal(expected, actual);
    }
}