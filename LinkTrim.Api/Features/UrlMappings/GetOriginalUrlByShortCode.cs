using LinkTrim.Api.Infrastructure.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LinkTrim.Api.Features.UrlMappings;

public static class GetOriginalUrlByShortCode
{
    public sealed class Query(string shortCode) : IRequest<string>
    {
        public string ShortCode { get; set; } = shortCode;
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Query, string>
    {
        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            return await appDbContext
                .UrlMappings
                .AsNoTracking()
                .Where(m => m.ShortCode == request.ShortCode)
                .Select(m => m.OriginalUrl)
                .FirstOrDefaultAsync(cancellationToken) ?? "not-found";
        }
    }
}