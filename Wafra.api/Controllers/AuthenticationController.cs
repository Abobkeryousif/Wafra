using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wafra.Application.Feature.Commands.RefreshToken;

namespace Wafra.api.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> RefreshToken(string token) 
        {
            return Ok(await _sender.Send(new RefreshTokenCommand(token)));
        }


private void SetRefreshTokenInCookie(string refreshToken, DateTime expier) 
{
    var cookieOption = new CookieOptions
    {
    HttpOnly = true,
    Expires = expier
    };
    Response.Cookies.Append("RefreshToken", refreshToken, cookieOption);

    }
    }
}
