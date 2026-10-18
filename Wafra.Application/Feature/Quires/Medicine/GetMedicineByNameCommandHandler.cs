using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Medicine;
using Wafra.Application.Observability.CustomTraces;
using Wafra.Application.Observability.Helper;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Medicine
{
    public record GetMedicineByNameCommand(GetByNameMedicineDto MedicineDto) : IRequest<HttpResult<string>> ;
    public class GetMedicineByNameCommandHandler : IRequestHandler<GetMedicineByNameCommand, HttpResult<string>>
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ILogger<GetMedicineByNameCommandHandler> _logger;

        public GetMedicineByNameCommandHandler(ILogger<GetMedicineByNameCommandHandler> logger ,IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
            _logger = logger;
        }

        public async Task<HttpResult<string>> Handle(GetMedicineByNameCommand request, CancellationToken cancellationToken)
        {
            const string module = "medicine";
            const string operation = "get_by_name";

            using var activity = MedicineActivitySource.Source.StartActivity("medicine.get_by_name");
            var stopwatch = Stopwatch.StartNew();

            if (string.IsNullOrWhiteSpace(request.MedicineDto.MedicineName))
            {
                _logger.LogWarning("user does't enter any value");
                activity?.SetTag("wafra.medicine.outcome", "invalid");
                BusinessMetricsHelper.RecordOperation(module, operation, "failure");

                return new HttpResult<string>(HttpStatusCode.BadRequest, "Please Enter Medicine Name");
            }

            var medicine = await _medicineRepository.FirstOrDefaultAsync(m=> m.Name.ToLower() == request.MedicineDto.MedicineName.ToLower());
            if (medicine == null)
            {
                _logger.LogWarning("medicine not found with name: {MedicineName}", request.MedicineDto.MedicineName);
                activity?.SetTag("wafra.medicine.outcome", "failure");
                BusinessMetricsHelper.RecordOperation(module, operation, "failure");

                return new HttpResult<string>(HttpStatusCode.NotFound, "Not Found Any Medicine");
            }
            stopwatch.Stop();

            _logger.LogInformation("medicine {MedicineName} retrieved successfly", medicine.Name);

            activity?.SetTag("wafra.medicine.outcome", "success");
            activity?.SetTag("wafra.medicine.matched_item", medicine.Name);

            BusinessMetricsHelper.RecordOperation(module, operation, "success");
            BusinessMetricsHelper.RecordDuration(module, operation, stopwatch.ElapsedMilliseconds);


            return new HttpResult<string>(HttpStatusCode.OK,"Seccuss Opration" , medicine.Name);
        }
    }
}
