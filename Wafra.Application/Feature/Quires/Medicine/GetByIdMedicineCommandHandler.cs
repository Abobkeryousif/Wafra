using MediatR;
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

        public GetByIdMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<HttpResult<MedicineDTO>> Handle(GetByIdMedicineCommand request, CancellationToken cancellationToken)
        {
            var result = await _medicineRepository.FirstOrDefaultAsync(m=> m.Id == request.Id);
            if (result == null) 
                return new HttpResult<MedicineDTO>(HttpStatusCode.NotFound, $"Not Found With ID:{request.Id}");
            var medicineDto = new MedicineDTO { Price = result.Price, Name = result.Name , CategoryId = result.CategoryId};
            return new HttpResult<MedicineDTO>(HttpStatusCode.OK , "Sccuess", medicineDto);
        }
    }
}
