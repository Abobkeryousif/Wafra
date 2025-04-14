using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Contracts.Services;
using Wafra.Core.Common;
using Wafra.Core.Common.Enum;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Commands.Authentication
{
    public record ForgetPasswordCommand(string email) : IRequest<HttpResult<string>>;
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand , HttpResult<string>>
    {
        private readonly ISendEmail _sendEmail;
        private readonly IUserRepository _userRepository;
        private readonly IVerificationRepository _verificationRepository;
        private readonly IConfiguration _configuration;
        public ForgetPasswordCommandHandler(ISendEmail sendEmail, IUserRepository userRepository, IVerificationRepository verificationRepository, IConfiguration configuration)
        {
            _sendEmail = sendEmail;
            _userRepository = userRepository;
            _verificationRepository = verificationRepository;
            _configuration = configuration;
        }

        public async Task<HttpResult<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.email);
            if (user == null)
                return new HttpResult<string>(HttpStatusCode.NotFound,$"Not Found This Email : {request.email}");

            var verifiaction = new Verification
            {
                Email = request.email,
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                TokenPerpoues = TokenPerpoues.RestPassword,
                ExpierOn = DateTime.Now.AddMinutes(30)
            };

            await _verificationRepository.CreateAsync(verifiaction);

            string url = $"{_configuration["AppUrl"]}Authentication/ResetPassword?email={verifiaction.Email}&token={verifiaction.Token}";
            _sendEmail.SendEmail(verifiaction.Email,"Reset Password" , url);

            return new HttpResult<string>(HttpStatusCode.OK,"Seccuss!","We Send Url In Your Email To Reset Password");
        }
    }
}



