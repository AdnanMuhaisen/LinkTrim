using ErrorOr;
using LinkTrim.Api.Core.Entities.UrlMappingAggregates.Errors;
using LinkTrim.Api.Dtos;
using LinkTrim.Api.Infrastructure.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Api.Features.UrlMappings;

public static class GetUrlMappingByOriginalUrl
{
    public sealed class Query(string url) : IRequest<ErrorOr<UrlMappingDto>>
    {
        public string Url { get; set; } = url;
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Query, ErrorOr<UrlMappingDto>>
    {
        public async Task<ErrorOr<UrlMappingDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var urlMapping = await appDbContext
                .UrlMappings
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OriginalUrl == request.Url, cancellationToken);

            if (urlMapping is not null)
            {
                UrlMappingDto urlMappingDto = new()
                {
                    Id = urlMapping.Id,
                    CreatedAt = urlMapping.CreatedAt,
                    ShortCode = urlMapping.ShortCode,
                    OriginalUrl = urlMapping.OriginalUrl,
                    ShortenedUrl = urlMapping.ShortenedUrl,
                    OriginalUrlHash = urlMapping.OriginalUrlHash
                };

                return urlMappingDto;
            }

            return UrlMappingErrors.NotFound;
        }
    }
}