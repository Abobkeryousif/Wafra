using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.Medicine
{
    public record DeleteMedicineCommand(int Id) : IRequest<HttpResult<string>>;
    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, HttpResult<string>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public DeleteMedicineCommandHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<HttpResult<string>> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            var result = await _medicineRepository.FirstOrDefaultAsync(m=> m.Id == request.Id);
            if (result == null)
                return new HttpResult<string>(HttpStatusCode.NotFound,$"Not Found With ID:{request.Id}");
            await _medicineRepository.DeleteAsync(result);
            return new HttpResult<string>(HttpStatusCode.OK, "Deleted Complete!",result.Name);
        }
    }
}
