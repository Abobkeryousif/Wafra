using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Core.Common;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Commands.Authentication
{
    public record LoginCommand(LoginUserDto users) : IRequest<HttpResult<AuthResult>>;
    public class LoginCommandHandler : IRequestHandler<LoginCommand, HttpResult<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LoginCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<HttpResult<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginUser = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.users.Email);
            if (loginUser == null)
                return new HttpResult<AuthResult>(HttpStatusCode.NotFound, "Email or Password Is Invalid");

            var pass = BCrypt.Net.BCrypt.Verify( request.users.Password,loginUser.Password);
            if (!pass)
                return new HttpResult<AuthResult>(HttpStatusCode.NotFound,"Email or Password Is Invalid");

            if (loginUser.IsValid == false)
                return new HttpResult<AuthResult>(HttpStatusCode.BadRequest, "Plaese Compelet Rigster To Login");

            var accessToken = _tokenRepository.CreateToken(loginUser);

            var refreshTokenUser = await _refreshTokenRepository.FirstOrDefaultAsync(t => t.userId == loginUser.Id, t => t.OrderByDescending(m => m.CreatedOn));
            if (refreshTokenUser.IsActive)
            {
                
                return new HttpResult<AuthResult>(HttpStatusCode.OK, "Login Success", new AuthResult
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshTokenUser.Token,
                    ExpierdOn = refreshTokenUser.ExpierOn
                });
            }

            var refreshToken = _tokenRepository.GenerateRefreshToken();
            refreshToken.userId = loginUser.Id;
            await _refreshTokenRepository.CreateAsync(refreshToken);

            return new HttpResult<AuthResult>(HttpStatusCode.OK,"Complete Login Opration" , new AuthResult 
            { 
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpierdOn = refreshToken.ExpierOn
            });
        }
    }
}
