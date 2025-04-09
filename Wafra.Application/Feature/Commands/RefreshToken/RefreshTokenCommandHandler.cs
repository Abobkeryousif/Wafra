using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.RefreshToken
{
    public record RefreshTokenCommand(string token) : IRequest<HttpResult<AuthResult>> ;
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, HttpResult<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
    public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

    public async Task<HttpResult<AuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.refreshTokens.Any(t => t.Token == request.token));
            if (user == null)
                return new HttpResult<AuthResult>(HttpStatusCode.NotFound, "InValid Token");

            var refreshToken = user.refreshTokens.Single(t=> t.Token == request.token);
            if (!refreshToken.IsActive) 
            {
                return new HttpResult<AuthResult>(HttpStatusCode.BadRequest, "Token Is Not Active");
            }

            refreshToken.RevokeOn = DateTime.UtcNow;
            var newRefreshToken = _tokenRepository.GenerateRefreshToken();
            user.refreshTokens.Add(newRefreshToken);
            await _userRepository.UpdateAsync(user);
            var token = _tokenRepository.CreateToken(user);

            return new HttpResult<AuthResult>(HttpStatusCode.OK, "Sccussafly Opration", new AuthResult
            { 
            AccessToken = token,
            RefreshToken = newRefreshToken.Token,
            ExpierdOn = newRefreshToken.ExpierOn
            });
            }

        }
    }
