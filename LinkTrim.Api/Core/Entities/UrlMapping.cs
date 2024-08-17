using LinkTrim.Api.Core.Entities.Abstraction;

namespace LinkTrim.Api.Core.Entities;

public class UrlMapping : IEntity, ISoftDeleteableEntity
{
    public int Id { get; set; }

    public string OriginalUrl { get; set; } = default!;

    public string ShortenedUrl { get; set; } = default!;

    public string ShortCode { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }
}