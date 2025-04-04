using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Pharmacy;
using Wafra.Core.Common;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Commands.Pharmacy
{
    public record CreatePharmacyCommand(PharmacyDTO PharmacyDTO) : IRequest<HttpResult<string>>;
    public class CreatePharmacyCommandHandler : IRequestHandler<CreatePharmacyCommand, HttpResult<string>>
    {
        private readonly IPharamcyRepository _pharamcyRepository;

        public CreatePharmacyCommandHandler(IPharamcyRepository pharamcyRepository)
        {
            _pharamcyRepository = pharamcyRepository;
        }

        public async Task<HttpResult<string>> Handle(CreatePharmacyCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _pharamcyRepository.IsExist(p=> p.Name == request.PharmacyDTO.Name);
            if (IsExist)
                return new HttpResult<string>(HttpStatusCode.BadRequest ,$"This Pharmacy Already Added! {request.PharmacyDTO.Name}");
            var pharmacy = new Pharmacies
            {
                Name = request.PharmacyDTO.Name,
                location = request.PharmacyDTO.location,
                Phone = request.PharmacyDTO.Phone
            };

            await _pharamcyRepository.CreateAsync(pharmacy);
            return new HttpResult<string>(HttpStatusCode.OK,"Pharmacy Add Sccussifly!",pharmacy.Name);
        }
    }
}
