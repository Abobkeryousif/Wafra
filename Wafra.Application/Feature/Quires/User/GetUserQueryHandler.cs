
using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.User
{
    public record GetUserQuery : IRequest<HttpResult<List<GetUser>>>;
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, HttpResult<List<GetUser>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResult<List<GetUser>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetALLAsync();
            if (result.Count == 0)
                return new HttpResult<List<GetUser>>(HttpStatusCode.NotFound,"Not Found Any User");
            var user = result.Select(u => new GetUser
            { 
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Phone = u.Phone,
            IsValid = u.IsValid,
            }).ToList();

            return new HttpResult<List<GetUser>>(HttpStatusCode.OK,"Compelte Opration",user);
        }
    }
}
