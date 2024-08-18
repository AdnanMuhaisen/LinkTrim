using LinkTrim.Api.Core.Entities.UrlMappingAggregates;
using LinkTrim.Api.Dtos;

namespace LinkTrim.Api.Mappers;

public interface IUrlMappingMapper
{
    UrlMapping Map(UrlMappingDto urlMappingDto);

    UrlMappingDto Map(UrlMapping urlMapping);
}

public class UrlMappingMapper : IUrlMappingMapper
{
    public UrlMapping Map(UrlMappingDto urlMappingDto)
    {
        return new()
        {
            Id = urlMappingDto.Id,
            CreatedAt = urlMappingDto.CreatedAt,
            ShortCode = urlMappingDto.ShortCode,
            OriginalUrl = urlMappingDto.OriginalUrl,
            ShortenedUrl = urlMappingDto.ShortenedUrl,
            OriginalUrlHash = urlMappingDto.OriginalUrlHash
        };
    }

    public UrlMappingDto Map(UrlMapping urlMapping)
    {
        return new()
        {
            Id = urlMapping.Id,
            CreatedAt = urlMapping.CreatedAt,
            ShortCode = urlMapping.ShortCode,
            OriginalUrl = urlMapping.OriginalUrl,
            ShortenedUrl = urlMapping.ShortenedUrl,
            OriginalUrlHash = urlMapping.OriginalUrlHash
        };
    }
}