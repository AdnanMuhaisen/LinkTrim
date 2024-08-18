using Asp.Versioning;
using LinkTrim.Api.Commands;
using LinkTrim.Api.Dtos;
using LinkTrim.Api.Features.UrlMappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkTrim.Api.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class UrlMappingsController(ISender sender) : ControllerBase
{
    [HttpPost("shorten")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<UrlMappingDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ShortenUrl(ShortenUrlCommand shortenUrlCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid url to shorten!");
        }

        var result = await sender.Send(new ShortenUrl.Command(
                shortenUrlCommand.OriginalUrl!,
                HttpContext.Request.Scheme,
                HttpContext.Request.Host.Value));

        return Ok(result);
    }
}