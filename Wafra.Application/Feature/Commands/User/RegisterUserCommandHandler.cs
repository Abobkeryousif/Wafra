using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Contracts.Services;
using Wafra.Application.Custom_Observability_Signals.CustomTraces;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Application.Observability.Helper;
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
        private readonly ILogger<RegisterUserCommandHandler> _logger;
    

        public RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger ,IUserRepository userRepository, ISendEmail sendEmail, IOtpRepository otpRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _sendEmail = sendEmail;
            _otpRepository = otpRepository;
            _tokenRepository = tokenRepository;
            _logger = logger;
        }

        public async Task<HttpResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            const string module = "user";
            const string operation = "register";

            using var activity = UserActivitySource.Source.StartActivity("user.register");
            Stopwatch stopwatch = Stopwatch.StartNew();

            
            var IsExist = await _userRepository.IsExist(u => u.Email == request.UserDto.Email);
            if (IsExist)
            {
                _logger.LogWarning("user registeritation rejected: email already exist");
                activity?.SetTag("wafra.user.register.outcome", "failure");
                BusinessMetricsHelper.RecordOperation(module, operation, "failure");

                return new HttpResult<string>(HttpStatusCode.BadRequest, $"This User Already Added! {request.UserDto.Name}");
            }

            var hashedPassword =  BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);

            var user = new Users
            {
                Name = request.UserDto.Name,
                Email = request.UserDto.Email,
                Phone = request.UserDto.Phone,
                Password = hashedPassword,
                IsValid = true

            };


            //otp Generate  
            //Random rendom = new Random();
            //int otp = rendom.Next(0, 999999);

            //var ConfirmOtps = new OTP
            //{
            //    Id = new Guid(),
            //    IsUsed = false,
            //    Otp = otp.ToString("000000"),
            //    UserEmail = user.Email,
            //    ExpriationOn = DateTime.Now.AddMinutes(5),
            //};



            //send email to user
            //_sendEmail.SendEmail(user.Email, subject: "Welcome To Wafra", message: $"Plaese Confirm Your Email By Add This Code \n\t\t" +
            //    $"{ConfirmOtps.Otp}");
            await _userRepository.CreateAsync(user);

            stopwatch.Stop();

            _logger.LogInformation("user {UserName} registerd successfly", user.Name);
            activity?.SetTag("wafra.user.outcome", "success");
            BusinessMetricsHelper.RecordOperation(module, operation, "success");

            BusinessMetricsHelper.RecordDuration(module, operation, stopwatch.ElapsedMilliseconds);

            //await _otpRepository.CreateAsync(ConfirmOtps);
            var token = _tokenRepository.CreateToken(user);
            return new HttpResult<string>(HttpStatusCode.OK, "User Registerd Successfly !");
        }
    }
}
