using LinkTrim.Api.Dtos;
using LinkTrim.Api.Features.UrlMappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkTrim.Api.Controllers.v1;

[ApiController]
[Route("api/[controller]")]
public class UrlMappingsController(ISender sender) : ControllerBase
{
    [HttpGet("shorten/{url}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<UrlMappingDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ShortenUrl([FromRoute] string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return BadRequest("Invalid url to shorten!");
        }

        var result = await sender.Send(new ShortenUrl.Command(
                Uri.UnescapeDataString(url),
                HttpContext.Request.Scheme,
                HttpContext.Request.Host.Value));

        return Ok(result);
    }
}