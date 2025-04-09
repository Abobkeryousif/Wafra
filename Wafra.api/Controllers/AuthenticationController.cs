using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wafra.api.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{

[HttpPost("Token")]    

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
