using Azure.Core;
using ErrorOr;
using LinkTrim.Api.Core.Entities.UrlMappingAggregates;
using LinkTrim.Api.Core.Interfaces;
using LinkTrim.Api.Dtos;
using LinkTrim.Api.Features.UrlMappings;
using LinkTrim.Api.Mappers;
using LinkTrim.Features.UnitTests.TestingDbSetup;
using MediatR;
using Moq;

namespace LinkTrim.Features.UnitTests.UrlMappings;

[Collection("LinkTrimDbCollection")]
public class ShortenUrlCommandHandlerTests
{
    private readonly LinkTrimTestDbFixture LinkTrimTestDbFixture;

    public ShortenUrlCommandHandlerTests(LinkTrimTestDbFixture linkTrimTestDbFixture)
        => (LinkTrimTestDbFixture) = (linkTrimTestDbFixture);

    [Fact]
    public async Task CommandHandler_AnExistingUrl_ReturnsAnExistingUrlMapping()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var urlHashingServiceMock = new Mock<IUrlHashingService>();
        var originalUrl = "https://devblogs.microsoft.com/dotnet/?c=34&WT.mc_id=dotnet-35129-website";
        var originalUrlHash = "$2a$10$abcdefghijklmnopqrstuuMJtaXn9eUax9B93VC1SUB3fzCID3x96";
        ErrorOr<UrlMappingDto> expected = new UrlMappingDto()
        {
            Id = 1,
            ShortCode = "70cb",
            CreatedAt = DateTime.Now,
            OriginalUrl = originalUrl,
            OriginalUrlHash = originalUrlHash,
            ShortenedUrl = "https://localhost:7237/url/70cb"
        };

        urlHashingServiceMock.Setup(service => service.GetHash(originalUrl)).Returns(originalUrlHash);
        senderMock.Setup(sender => sender.Send(It.IsAny<GetUrlMappingByOriginalUrl.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);
        using var context = LinkTrimTestDbFixture.CreateContext();
        var command = new ShortenUrl.Command(originalUrl, null!, null!);
        var handler = new ShortenUrl.Handler(senderMock.Object, context, null!, urlHashingServiceMock.Object);

        // Act
        var actual = await handler.Handle(command, default);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CommandHandler_GenerateNewUrlMapping_ReturnsTheCreatedUrlMapping()
    {
        // Arrange
        var senderMock = new Mock<ISender>();
        var urlHashingServiceMock = new Mock<IUrlHashingService>();
        var urlMappingMapperMock = new Mock<IUrlMappingMapper>();
        var originalUrl = "https://react.dev/learn";
        urlHashingServiceMock.Setup(service => service.GetHash(originalUrl)).Returns("test_hash_value");
        urlMappingMapperMock.Setup(mapper => mapper.Map(It.IsAny<UrlMapping>())).Returns(new UrlMappingDto() { Id = 1 });
        using var context = LinkTrimTestDbFixture.CreateContext();
        var command = new ShortenUrl.Command(originalUrl, Uri.UriSchemeHttps, "https://localhost:7237");
        var handler = new ShortenUrl.Handler(senderMock.Object, context, urlMappingMapperMock.Object, urlHashingServiceMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.Id > 0);
    }
}