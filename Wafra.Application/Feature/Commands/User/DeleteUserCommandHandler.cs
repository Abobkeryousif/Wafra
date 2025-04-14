using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.User
{
    public record DeleteUserCommand(int Id) : IRequest<HttpResult<string>>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, HttpResult<string>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResult<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (result == null)
                return new HttpResult<string>(HttpStatusCode.NotFound,$"Not Found With ID:{request.Id}");

            await _userRepository.DeleteAsync(result);

            return new HttpResult<string>(HttpStatusCode.OK, "Sccuss Opration",result.Name);
            
        }
    }
}
