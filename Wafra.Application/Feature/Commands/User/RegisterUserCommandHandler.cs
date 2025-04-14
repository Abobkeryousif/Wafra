using MediatR;
using MimeKit.Cryptography;
using System.Net;
using System.Security.Cryptography;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Contracts.Services;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Core.Common;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Commands.User
{
    public record RegisterUserCommand(UserDto UserDto) : IRequest<HttpResult<string>>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, HttpResult<string>>
    {
        private readonly ISendEmail _sendEmail;
        private readonly IUserRepository _userRepository;
        private readonly IOtpRepository _otpRepository;
        private readonly ITokenRepository _tokenRepository;
    

        public RegisterUserCommandHandler(IUserRepository userRepository, ISendEmail sendEmail, IOtpRepository otpRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _sendEmail = sendEmail;
            _otpRepository = otpRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<HttpResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _userRepository.IsExist(u => u.Email == request.UserDto.Email);
            if (IsExist)
                return new HttpResult<string>(HttpStatusCode.BadRequest, $"This User Already Added! {request.UserDto.Name}");
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
            var user = new Users
            {
                Name = request.UserDto.Name,
                Email = request.UserDto.Email,
                Phone = request.UserDto.Phone,
                Password = hashPassword,
                
            };


            //otp Generate  
            Random rendom = new Random();
            int otp = rendom.Next(0, 999999);

            var ConfirmOtps = new OTP
            {
                Id = new Guid(),
                IsUsed = false,
                Otp = otp.ToString("000000"),
                UserEmail = user.Email,
                ExpriationOn = DateTime.Now.AddMinutes(5),
            };



            //send email to user
            _sendEmail.SendEmail(user.Email, subject: "Welcome To Wafra", message: $"Plaese Confirm Your Email By Add This Code \n\t\t" +
                $"{ConfirmOtps.Otp}");
            await _userRepository.CreateAsync(user);
            await _otpRepository.CreateAsync(ConfirmOtps);
            var token = _tokenRepository.CreateToken(user);
            return new HttpResult<string>(HttpStatusCode.OK, "We Send Otp in Email Plaese Confirem It");
        }
    }
}
