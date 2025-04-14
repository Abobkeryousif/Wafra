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
using static System.Net.Mime.MediaTypeNames;

namespace Wafra.Application.Feature.Quires.User
{
    public record GetByIdUserCommand(int Id) : IRequest<HttpResult<GetUser>>;
    public class GetByIdUserCommandHandler : IRequestHandler<GetByIdUserCommand, HttpResult<GetUser>>
    {
        private readonly IUserRepository _userRepository;

        public GetByIdUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResult<GetUser>> Handle(GetByIdUserCommand request, CancellationToken cancellationToken)
        {
            var reslut = await _userRepository.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (reslut == null)
                return new HttpResult<GetUser>(HttpStatusCode.NotFound, $"Not Found With ID:{request.Id}");
            var user = new GetUser
            {
                Id = reslut.Id,
                Name = reslut.Name,
                Email = reslut.Email,
                Phone = reslut.Phone,
                IsValid = reslut.IsValid,
            };

            return new HttpResult<GetUser>(HttpStatusCode.OK,"Sccuss!",user);
        }
    }
}
