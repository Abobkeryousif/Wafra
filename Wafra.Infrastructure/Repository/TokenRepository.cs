using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wafra.Application.Contracts.Services;
using Wafra.Core.Entites;

namespace Wafra.Infrastructure.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Users users)
        {
            var cliams = new List<Claim>();
            cliams.Add(new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()));
            cliams.Add(new Claim(ClaimTypes.Email, users.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var Cerdintial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                _configuration["JWT:Issure"],
                _configuration["JWT:Audience"],
                cliams,
                signingCredentials : Cerdintial,
                expires: DateTime.UtcNow.AddMinutes(30)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
