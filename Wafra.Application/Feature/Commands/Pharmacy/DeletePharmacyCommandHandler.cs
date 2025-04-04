using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.Pharmacy
{
    public record DeletePharmacyCommand(int Id) : IRequest<HttpResult<string>>;
    public class DeletePharmacyCommandHandler : IRequestHandler<DeletePharmacyCommand, HttpResult<string>>
    {
        private readonly IPharamcyRepository _pharamcyRepository;

        public DeletePharmacyCommandHandler(IPharamcyRepository pharamcyRepository)
        {
            _pharamcyRepository = pharamcyRepository;
        }

        public async Task<HttpResult<string>> Handle(DeletePharmacyCommand request, CancellationToken cancellationToken)
        {
            var result = await _pharamcyRepository.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (result == null)
                return new HttpResult<string>(HttpStatusCode.NotFound,$"Not Found With ID:{request.Id}");
            await _pharamcyRepository.DeleteAsync(result);
            return new HttpResult<string>(HttpStatusCode.OK,"Deleted Sccussfaly!",result.Name);
        }
    }
}
