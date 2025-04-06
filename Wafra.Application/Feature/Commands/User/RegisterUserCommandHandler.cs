using MediatR;
using MimeKit.Cryptography;
using System.Net;
using System.Net.Mail;
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

        public RegisterUserCommandHandler(IUserRepository userRepository, ISendEmail sendEmail)
        {
            _userRepository = userRepository;
            _sendEmail = sendEmail;
        }

        public async Task<HttpResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _userRepository.IsExist(u => u.Name == request.UserDto.Name);
            if (IsExist)
                return new HttpResult<string>(HttpStatusCode.BadRequest, $"This User Already Added! {request.UserDto.Name}");
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
            var user = new Users
            {
                Name = request.UserDto.Name,
                Email = request.UserDto.Email,
                Phone = request.UserDto.Phone,
                Password = hashPassword,
                IsValid = request.UserDto.IsValid,
            };

            var newOtp = Generate();
            var ConfirmOtps = new OTP
            {
                Id = new Guid(),
                IsUsed = false,
                Otp = newOtp,
                UserEmail = user.Email
            };

            _sendEmail.SendEmail(user.Email, subject: "Welcome To Wafra", message: $"Plaese Confirm Your Email By Add This Code :\n" +
                $"{ConfirmOtps.Otp}");
            await _userRepository.CreateAsync(user);
            return new HttpResult<string>(HttpStatusCode.OK, "User Add Sccussfaly!", user.Name);
        }


        private string Generate(int lenght = 6)
        {
            var random = new Random();
            string otp = "";
            for (int i = 0; i < lenght; i++)
            {
                otp += random.Next(0, 10);
            }
            return otp;
        }
}


}
