using MediatR;
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

        public GetMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<HttpResult<List<GetMedicineDto>>> Handle(GetMedicineCommand request, CancellationToken cancellationToken)
        {
            var result = await _medicineRepository.GetALLAsync();
            if (result.Count == 0)
                return new HttpResult<List<GetMedicineDto>>(HttpStatusCode.NotFound,"Medicine Not Found");
            var medicine = result.Select(m=> new GetMedicineDto { Id = m.Id , Name = m.Name , Price = m.Price , CategoryId = m.CategoryId}).ToList();
            return new HttpResult<List<GetMedicineDto>>(HttpStatusCode.OK,"All Medicine Returned",medicine);

        }
    }
}
