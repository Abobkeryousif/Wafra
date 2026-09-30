using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Medicin;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Medicine
{
    public record GetByIdMedicineCommand(int Id) : IRequest<HttpResult<MedicineDTO>>;
    public class GetByIdMedicineCommandHandler : IRequestHandler<GetByIdMedicineCommand, HttpResult<MedicineDTO>>
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ILogger<GetByIdMedicineCommandHandler> _logger;

        public GetByIdMedicineCommandHandler(IMedicineRepository medicineRepository, ILogger<GetByIdMedicineCommandHandler> logger)
        {
            _medicineRepository = medicineRepository;
            _logger = logger;
        }

        public async Task<HttpResult<MedicineDTO>> Handle(GetByIdMedicineCommand request, CancellationToken cancellationToken)
        {
            var result = await _medicineRepository.FirstOrDefaultAsync(m=> m.Id == request.Id);
            if (result == null)
            {
                _logger.LogWarning("No medicine matched selected id: {id}", request.Id);
                return new HttpResult<MedicineDTO>(HttpStatusCode.NotFound, $"Not Found With ID:{request.Id}");
            }

            var medicineDto = new MedicineDTO { Price = result.Price, Name = result.Name , CategoryId = result.CategoryId};
            _logger.LogInformation("Successfly retrived Medicine: {MedicineNamn}", medicineDto.Name);

            return new HttpResult<MedicineDTO>(HttpStatusCode.OK , "Sccuess", medicineDto);
        }
    }
}
