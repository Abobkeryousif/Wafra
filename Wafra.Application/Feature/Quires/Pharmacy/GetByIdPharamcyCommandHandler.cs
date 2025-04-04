using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Pharmacy;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Pharmacy
{
    public record GetByIdPharamcyCommand(int Id) : IRequest<HttpResult<GetPharmacy>>;

    public class GetByIdPharamcyCommandHandler : IRequestHandler<GetByIdPharamcyCommand, HttpResult<GetPharmacy>>
    {
        private readonly IPharamcyRepository _pharamcyRepository;

        public GetByIdPharamcyCommandHandler(IPharamcyRepository pharamcyRepository)
        {
            _pharamcyRepository = pharamcyRepository;
        }

        public async Task<HttpResult<GetPharmacy>> Handle(GetByIdPharamcyCommand request, CancellationToken cancellationToken)
        {
            var result = await _pharamcyRepository.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (result == null) 
                return new HttpResult<GetPharmacy>(HttpStatusCode.NotFound,$"Not Found With ID:{request.Id}");
            var pharmacy = new GetPharmacy
            {
                Id = result.Id,
                Name = result.Name,
                location = result.location,
                Phone = result.Phone
            };
            
            return new HttpResult<GetPharmacy>(HttpStatusCode.OK, "Sccuss!",pharmacy);
        }
    }
}
