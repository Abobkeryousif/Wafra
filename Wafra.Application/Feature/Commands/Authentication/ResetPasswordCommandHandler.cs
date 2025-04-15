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
    public record ResetPasswordCommand(ResetPasswordDTO PasswordDTO) : IRequest<HttpResult<GetUserDto>>;
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, HttpResult<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationRepository _verificationRepository;

        public ResetPasswordCommandHandler(IVerificationRepository verificationRepository, IUserRepository userRepository)
        {
            _verificationRepository = verificationRepository;
            _userRepository = userRepository;
        }

        public async Task<HttpResult<GetUserDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.PasswordDTO.email);
            if (user == null)
                return new HttpResult<GetUserDto>(HttpStatusCode.NotFound,"Not Found User");

            var verification = await _verificationRepository.FirstOrDefaultAsync(v=> v.Email == user.Email);
            if (verification.Token != request.PasswordDTO.token)
                return new HttpResult<GetUserDto>(HttpStatusCode.BadRequest,"Token Invalid");

            if (verification.IsExpier)
                return new HttpResult<GetUserDto>(HttpStatusCode.BadRequest,"Token Is Expier");

            if (verification.IsUsed)
                return new HttpResult<GetUserDto>(HttpStatusCode.BadRequest, "Token Already Used");
            
            //hash Password And Add To User
            string newPassword = BCrypt.Net.BCrypt.HashPassword(request.PasswordDTO.password);
            user.Password = newPassword;

            await _userRepository.UpdateAsync(user);
            verification.IsUsed = true;
            await _verificationRepository.UpdateAsync(verification);

            
            return new HttpResult<GetUserDto>(HttpStatusCode.OK,"Update Password Successfaly!", new GetUserDto 
            {
                 Email = user.Email,Phone = user.Phone,Name=user.Name
            });

        }
    }
}
