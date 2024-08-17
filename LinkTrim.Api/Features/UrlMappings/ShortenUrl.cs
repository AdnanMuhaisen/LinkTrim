using LinkTrim.Api.Core.Entities.UrlMappingAggregates;
using LinkTrim.Api.Core.Interfaces;
using LinkTrim.Api.Dtos;
using LinkTrim.Api.Infrastructure.Data.Contexts;
using LinkTrim.Api.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Api.Features.UrlMappings;

public static class ShortenUrl
{
    public sealed class Command(string originalUrl, string scheme, string hostName) : IRequest<UrlMappingDto>
    {
        public string OriginalUrl { get; set; } = originalUrl;

        public string Scheme { get; set; } = scheme;

        public string HostName { get; set; } = hostName;
    }

    public class Handler(ISender sender, AppDbContext appDbContext, IUrlMappingMapper urlMappingMapper, IUrlHashingService urlHashingService) : IRequestHandler<Command, UrlMappingDto>
    {
        public async Task<UrlMappingDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var targetUrlHash = urlHashingService.GetHash(request.OriginalUrl);
            var isUrlExists = await appDbContext
                .UrlMappings
                .AsNoTracking()
                .AnyAsync(m => m.OriginalUrlHash == targetUrlHash, cancellationToken);
            if (isUrlExists)
            {
                var result = await sender.Send(new GetUrlMappingByOriginalUrl.Query(request.OriginalUrl), cancellationToken);

                return result.Value;
            }

            // Generate a url mapping for the url
            var shortCode = Guid.NewGuid().ToString()[..4];
            var shortenedUrl = $"{request.Scheme}://{request.HostName}/url/{shortCode}";

            UrlMapping addedUrlMapping = new()
            {
                ShortCode = shortCode,
                ShortenedUrl = shortenedUrl,
                CreatedAt = DateTime.UtcNow,
                OriginalUrlHash = targetUrlHash,
                OriginalUrl = request.OriginalUrl
            };

            await appDbContext.UrlMappings.AddAsync(addedUrlMapping, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return urlMappingMapper.Map(addedUrlMapping);
        }
    }
}