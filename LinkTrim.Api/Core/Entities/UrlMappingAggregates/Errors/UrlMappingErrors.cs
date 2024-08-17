using ErrorOr;

namespace LinkTrim.Api.Core.Entities.UrlMappingAggregates.Errors;

public static class UrlMappingErrors
{
    public static Error NotFound = Error.NotFound("UrlMapping.NotFound", "Can not find a map for this url");
}