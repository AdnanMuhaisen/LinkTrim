namespace LinkTrim.Api.Dtos;

public record UrlMappingDto
{
    public int Id { get; set; }

    public string OriginalUrl { get; set; } = default!;

    public string OriginalUrlHash { get; set; } = default!;

    public string ShortenedUrl { get; set; } = default!;

    public string ShortCode { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
}