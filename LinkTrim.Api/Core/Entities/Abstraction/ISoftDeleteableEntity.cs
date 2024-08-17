namespace LinkTrim.Api.Core.Entities.Abstraction;

public interface ISoftDeleteableEntity
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }
}