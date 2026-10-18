using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Custom_Observability_Signals.CustomTraces;
using Wafra.Application.Feature.DTOs.Pharmacy;
using Wafra.Application.Observability.Helper;
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
            const string module = "pharmacy";
            const string operation = "get_all";

            using var activity = PharmacyActivitySource.Source.StartActivity("pharmacy.get_all");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var result = await _pharamcyRepository.GetALLAsync();
            if (result.Count == 0)
            {
                _logger.LogWarning("Not found Pharmacy in DB.");
                activity?.SetTag("wafra.pharmacy.outcome", "failure");

                BusinessMetricsHelper.RecordOperation(module, operation, "failure");

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

            activity?.SetTag("wafra.pharmacy.outcome", "success");
            activity?.SetStatus(ActivityStatusCode.Ok);

            BusinessMetricsHelper.RecordOperation(module, operation, "success");
            BusinessMetricsHelper.RecordDuration(module, operation, stopwatch.ElapsedMilliseconds);

            return new HttpResult<List<GetPharmacy>>(HttpStatusCode.OK, "Sccussfly Opration",pharmacy);
        }
    }
}
