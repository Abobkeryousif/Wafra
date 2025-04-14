using AutoMapper;
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
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Quires.Pharmacy
{

    public class GetAllPharmacyCommand() : IRequest<HttpResult<List<GetPharmacy>>>;

    public class GetPharmacyCommandHandler : IRequestHandler<GetAllPharmacyCommand, HttpResult<List<GetPharmacy>>>
    {
        
        private readonly IPharamcyRepository _pharamcyRepository;

        public GetPharmacyCommandHandler(IPharamcyRepository pharamcyRepository)
        {
            _pharamcyRepository = pharamcyRepository;
        }

        public async Task<HttpResult<List<GetPharmacy>>> Handle(GetAllPharmacyCommand request, CancellationToken cancellationToken)
        {
            var result = await _pharamcyRepository.GetALLAsync();
            if (result.Count == 0)
                return new HttpResult<List<GetPharmacy>>(HttpStatusCode.NotFound,"Not Found Pharmacies!");
            var pharmacy = result.Select(p => new GetPharmacy
            {
                Id = p.Id,
                location = p.location,
                Name = p.Name,
                Phone = p.Phone,

            }).ToList();
            return new HttpResult<List<GetPharmacy>>(HttpStatusCode.OK, "Sccussfly Opration",pharmacy);
        }
    }
}
