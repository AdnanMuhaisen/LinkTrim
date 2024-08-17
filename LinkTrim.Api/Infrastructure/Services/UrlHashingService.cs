using LinkTrim.Api.Core.Interfaces;

namespace LinkTrim.Api.Infrastructure.Services;

public class UrlHashingService(IConfiguration configuration) : IUrlHashingService
{
    public string GetHash(string url)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);

        return BCrypt.Net.BCrypt.HashPassword(url, configuration["Salt"]);
    }
}