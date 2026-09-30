using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Pharmacy;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Pharmacy
{

    public class GetAllPharmacyCommand() : IRequest<HttpResult<List<GetPharmacy>>>;

    public class GetPharmacyCommandHandler : IRequestHandler<GetAllPharmacyCommand, HttpResult<List<GetPharmacy>>>
    {
        
        private readonly IPharamcyRepository _pharamcyRepository;
        private readonly ILogger<GetPharmacyCommandHandler> _logger;

        public GetPharmacyCommandHandler(IPharamcyRepository pharamcyRepository, ILogger<GetPharmacyCommandHandler> logger)
        {
            _pharamcyRepository = pharamcyRepository;
            _logger = logger;
        }

        public async Task<HttpResult<List<GetPharmacy>>> Handle(GetAllPharmacyCommand request, CancellationToken cancellationToken)
        {
            var result = await _pharamcyRepository.GetALLAsync();
            if (result.Count == 0)
            {
                _logger.LogWarning("Not found Pharmacy in DB.");
                return new HttpResult<List<GetPharmacy>>(HttpStatusCode.NotFound, "Not Found Pharmacies!");
            }
            var pharmacy = result.Select(p => new GetPharmacy
            {
                Id = p.Id,
                location = p.location,
                Name = p.Name,
                Phone = p.Phone,

            }).ToList();

            _logger.LogInformation("Successfly retrive: {PharmacyCount} Pharmacys", result.Count);
            return new HttpResult<List<GetPharmacy>>(HttpStatusCode.OK, "Sccussfly Opration",pharmacy);
        }
    }
}
