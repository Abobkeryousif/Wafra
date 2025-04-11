using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Common;
using Wafra.Core.Entites;
using Wafra.Infrastructure.Data;

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
                expires: DateTime.Now.AddMinutes(1)
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

        public RefreshToken GenerateRefreshToken()
        {
            var random = new byte[32];
            using var genretor = new RNGCryptoServiceProvider();
            genretor.GetBytes(random);
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Convert.ToBase64String(random),
                ExpierOn = DateTime.Now.AddDays(5),
                CreatedOn = DateTime.Now,
            };
        }
    }
}




