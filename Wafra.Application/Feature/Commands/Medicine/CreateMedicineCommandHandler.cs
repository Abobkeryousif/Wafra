using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Medicin;
using Wafra.Core.Common;
using Wafra.Core.Entites;
namespace Wafra.Application.Feature.Commands.Medicin
{
    public record CreateMedicinCommand(MedicineDTO MedicineDTO) : IRequest<HttpResult<MedicineDTO>>;
    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicinCommand, HttpResult<MedicineDTO>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public CreateMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<HttpResult<MedicineDTO>> Handle(CreateMedicinCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _medicineRepository.IsExist(m=> m.Name == request.MedicineDTO.Name);
            if (IsExist)
                return new HttpResult<MedicineDTO>(HttpStatusCode.BadRequest, $"This Medicine Already Added! {request.MedicineDTO.Name}");
            var medicine = new Medicines
            {
                Name = request.MedicineDTO.Name,
                Price = request.MedicineDTO.Price,
                CategoryId = request.MedicineDTO.CategoryId
            };
            await _medicineRepository.CreateAsync(medicine);
            return new HttpResult<MedicineDTO>(HttpStatusCode.OK ,"Medicine Add Sccussifly!",request.MedicineDTO);
        }
    }
}
