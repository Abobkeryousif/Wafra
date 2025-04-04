using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Medicin;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.Medicine
{
    public record UpdateMedicineCommand(int Id , MedicineDTO MedicineDTO)  :IRequest<HttpResult<MedicineDTO>>;
    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, HttpResult<MedicineDTO>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public UpdateMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<HttpResult<MedicineDTO>> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            var result = await _medicineRepository.FirstOrDefaultAsync(m=>m.Id == request.Id);
            if (result == null)
                return new HttpResult<MedicineDTO>(HttpStatusCode.NotFound,$"Not Found With ID:{request.Id}");
            result.Name = request.MedicineDTO.Name;
            result.Price = request.MedicineDTO.Price;
            result.CategoryId = request.MedicineDTO.CategoryId;
            await _medicineRepository.UpdateAsync(result);
            return new HttpResult<MedicineDTO>(HttpStatusCode.OK,"Update Sccussfaly!",request.MedicineDTO);
        }
    }
}
