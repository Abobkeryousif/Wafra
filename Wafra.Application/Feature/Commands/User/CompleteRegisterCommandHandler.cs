
using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.User
{
    public record CompleteRegisterCommand(UserOTP OTP) : IRequest<HttpResult<AuthResult>>;
    public class CompleteRegisterCommandHandler : IRequestHandler<CompleteRegisterCommand, HttpResult<AuthResult>>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IOtpRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public CompleteRegisterCommandHandler(ITokenRepository tokenRepository, IOtpRepository otpRepository, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenRepository = tokenRepository;
            _otpRepository = otpRepository;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<HttpResult<AuthResult>> Handle(CompleteRegisterCommand request, CancellationToken cancellationToken)
        {
            var otp = await _otpRepository.FirstOrDefaultAsync(c => c.Otp == request.OTP.OTPCode);
            if (otp == null)
                return new HttpResult<AuthResult>(HttpStatusCode.NotFound, "Otp Is Invalid");

            if(otp.IsExpired)
                return new HttpResult<AuthResult>(HttpStatusCode.NotFound, "Otp Is Exiperd");

            if(otp.IsUsed)
                return new HttpResult<AuthResult>(HttpStatusCode.NotFound, "Otp Is Already Used");


            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == otp.UserEmail);

            user.IsValid = true;
            otp.IsUsed = true;

            var refreshToken = _tokenRepository.GenerateRefreshToken();
            refreshToken.userId = user.Id;
            await _refreshTokenRepository.CreateAsync(refreshToken);
            var accessToken = _tokenRepository.CreateToken(user);

            await _userRepository.UpdateAsync(user);
            await _otpRepository.UpdateAsync(otp);

            return new HttpResult<AuthResult>(HttpStatusCode.OK, "Complete Register Sccussifly!", new AuthResult 
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpierdOn = refreshToken.ExpierOn
            });
        }
    }
}
