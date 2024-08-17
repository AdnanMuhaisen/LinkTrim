using Asp.Versioning;
using LinkTrim.Api.Features.UrlMappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkTrim.Api.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("[controller]")]
public class UrlController(ISender sender) : ControllerBase
{
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> Get([FromRoute] string shortCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(shortCode);

        return Redirect((await sender.Send(new GetOriginalUrlByShortCode.Query(shortCode))));
    }
}