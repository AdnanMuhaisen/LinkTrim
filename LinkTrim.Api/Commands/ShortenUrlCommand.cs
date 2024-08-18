using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace LinkTrim.Api.Commands;

public class ShortenUrlCommand
{
    [FromQuery(Name = "originalUrl")]
    public string? OriginalUrl { get; set; }
}

public class ShortenUrlCommandValidator : AbstractValidator<ShortenUrlCommand>
{
    public ShortenUrlCommandValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty()
            .Must(IsValidUrl);
    }

    private bool IsValidUrl(string? url)
        => Uri.TryCreate(url, UriKind.Absolute, out Uri? result)
           && result is not null
           && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
}