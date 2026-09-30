using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Medicin;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Medicine
{
    public record GetMedicineCommand() : IRequest<HttpResult<List<GetMedicineDto>>>;
    public class GetMedicineCommandHandler : IRequestHandler<GetMedicineCommand, HttpResult<List<GetMedicineDto>>>
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ILogger<GetMedicineCommandHandler> _logger;

        public GetMedicineCommandHandler(IMedicineRepository medicineRepository, ILogger<GetMedicineCommandHandler> logger)
        {
            _medicineRepository = medicineRepository;
            _logger = logger;
        }

        public async Task<HttpResult<List<GetMedicineDto>>> Handle(GetMedicineCommand request, CancellationToken cancellationToken)
        {

            
            var result = await _medicineRepository.GetALLAsync();

            if (result.Count == 0)
            {
                _logger.LogWarning("No medicine found in DB.");
                return new HttpResult<List<GetMedicineDto>>(HttpStatusCode.NotFound, "Medicine Not Found");
            }

            var medicine = result.Select(m=> new GetMedicineDto { Id = m.Id , Name = m.Name , Price = m.Price , CategoryId = m.CategoryId}).ToList();

            _logger.LogInformation("Successfly retrived {MedicineCount} medicines.", result.Count);

            return new HttpResult<List<GetMedicineDto>>(HttpStatusCode.OK,"All Medicine Returned",medicine);

        }
    }
}
