using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wafra.Application.Feature.Commands.Authentication;
using Wafra.Application.Feature.Commands.RefreshToken;
using Wafra.Application.Feature.DTOs.User;

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

        [HttpPost("login")]

        public async Task<IActionResult> LoginAsync(LoginUserDto login) 
        {
            return Ok(await _sender.Send(new LoginCommand(login)));
        }

        [HttpPost("ForgetPassword")]

        public async Task<IActionResult> ForgetPasswordAsync(string email) 
        {
            return Ok(await _sender.Send(new ForgetPasswordCommand(email)));
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



