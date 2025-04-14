using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.User
{
    public record UpdateUserCommand(int Id , UserDto UserDto): IRequest<HttpResult<UserDto>>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, HttpResult<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResult<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (result == null)

                return new HttpResult<UserDto>(HttpStatusCode.NotFound, $"Not Found With ID:{request.Id}");

            request.UserDto.Password = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
            result.Name = request.UserDto.Name;
            result.Phone = request.UserDto.Phone;
            result.Email = request.UserDto.Email;
            result.Password = request.UserDto.Password;
            result.IsValid = true;
            await _userRepository.UpdateAsync(result);

            return new HttpResult<UserDto>(HttpStatusCode.OK,"User Updated Sccussfaly!",request.UserDto);
            
        }
    }
}
