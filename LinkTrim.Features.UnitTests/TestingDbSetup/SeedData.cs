using LinkTrim.Api.Core.Entities.UrlMappingAggregates;

namespace LinkTrim.Features.UnitTests.TestingDbSetup;

public static class SeedData
{
    public static IEnumerable<UrlMapping> GetUrlMappings()
    {
        return
        [
            new (){
                DeletedAt = null,
                IsDeleted = false,
                ShortCode = "70cb",
                CreatedAt = DateTime.Now,
                ShortenedUrl = "https://localhost:7237/url/70cb",
                OriginalUrlHash = "$2a$10$abcdefghijklmnopqrstuuMJtaXn9eUax9B93VC1SUB3fzCID3x96",
                OriginalUrl = "https://devblogs.microsoft.com/dotnet/?c=34&WT.mc_id=dotnet-35129-website"
            },
            new (){
                DeletedAt = null,
                IsDeleted = false,
                ShortCode = "91ae",
                CreatedAt = DateTime.Now,
                ShortenedUrl = "https://localhost:7237/url/91ae",
                OriginalUrlHash = "$10$abcdefghijklmnopqrstuuj0RIi6FijvoTqb5PJ8CQK.SJWSUGtma",
                OriginalUrl = "https://github.com/AdnanMuhaisen/LinkTrim/tree/main/LinkTrim.Api"
            }
        ];
    }
}