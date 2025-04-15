using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Medicine;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Medicine
{
    public record GetMedicineByNameCommand(GetByNameMedicineDto MedicineDto) : IRequest<HttpResult<string>> ;
    public class GetMedicineByNameCommandHandler : IRequestHandler<GetMedicineByNameCommand, HttpResult<string>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicineByNameCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<HttpResult<string>> Handle(GetMedicineByNameCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.MedicineDto.MedicineName))
                return new HttpResult<string>(HttpStatusCode.BadRequest,"Please Enter Medicine Name");

            var medicine = await _medicineRepository.FirstOrDefaultAsync(m=> m.Name.ToLower() == request.MedicineDto.MedicineName.ToLower());
            if (medicine == null)
                return new HttpResult<string>(HttpStatusCode.NotFound,"Not Found Any Medicine");

            return new HttpResult<string>(HttpStatusCode.OK,"Seccuss Opration" , medicine.Name);
        }
    }
}
