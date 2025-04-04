using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wafra.Application.Feature.Commands.Medicin;
using Wafra.Application.Feature.Commands.Medicine;
using Wafra.Application.Feature.DTOs.Medicin;
using Wafra.Application.Feature.Quires.Medicine;

namespace Wafra.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ISender _sender;

        public MedicineController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]

        public async Task<IActionResult> CreateAsync(MedicineDTO medicineDTO)
        {
            return Ok(await _sender.Send(new CreateMedicinCommand(medicineDTO)));
        }
        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _sender.Send(new GetMedicineCommand()));
        }
        [HttpGet("GetById")]

        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            return Ok(await _sender.Send(new GetByIdMedicineCommand(Id)));
        }

        [HttpPut("Update")]

        public async Task<IActionResult> UpdateAsync(int Id, MedicineDTO medicineDTO)
        {
            return Ok(await _sender.Send(new UpdateMedicineCommand(Id, medicineDTO)));
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteAsync(int Id)
        {
            return Ok(await _sender.Send(new DeleteMedicineCommand(Id)));
        }



    }
}





