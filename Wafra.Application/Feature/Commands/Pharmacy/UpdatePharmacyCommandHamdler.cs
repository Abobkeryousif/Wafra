

using MediatR;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Pharmacy;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.Pharmacy
{
    public record UpdatePharmacyCommand(int Id,PharmacyDTO PharmacyDTO) : IRequest<HttpResult<PharmacyDTO>>;

    public class UpdatePharmacyCommandHamdler : IRequestHandler<UpdatePharmacyCommand, HttpResult<PharmacyDTO>>
    {
        private readonly IPharamcyRepository _pharamcyRepository;

        public UpdatePharmacyCommandHamdler(IPharamcyRepository pharamcyRepository)
        {
            _pharamcyRepository = pharamcyRepository;
        }

        public async Task<HttpResult<PharmacyDTO>> Handle(UpdatePharmacyCommand request, CancellationToken cancellationToken)
        {
            var result = await _pharamcyRepository.FirstOrDefaultAsync(p=>p.Id == request.Id);
            if (result == null)
                return new HttpResult<PharmacyDTO>(HttpStatusCode.NotFound,$"Not Found with ID:{request.Id}");
            result.Name = request.PharmacyDTO.Name;
            result.Phone = request.PharmacyDTO.Phone;
            result.location = request.PharmacyDTO.location;
            await _pharamcyRepository.UpdateAsync(result);
            return new HttpResult<PharmacyDTO>(HttpStatusCode.OK,"Updated Sccussfaly",request.PharmacyDTO);
        }
    }
}
