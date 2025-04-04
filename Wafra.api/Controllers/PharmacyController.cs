using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wafra.Application.Feature.Commands.Pharmacy;
using Wafra.Application.Feature.DTOs.Pharmacy;
using Wafra.Application.Feature.Quires.Pharmacy;

namespace Wafra.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly ISender _sender;

        public PharmacyController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]

        public async Task<IActionResult> CreateAsync(PharmacyDTO pharmacyDTO) 
        {
            var result = await _sender.Send(new CreatePharmacyCommand(pharmacyDTO));
            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _sender.Send(new GetAllPharmacyCommand()));
        }
        [HttpGet("GetById")]
        
        public async Task<IActionResult> GetByIdAsync(int Id) 
        {
            return Ok(await _sender.Send(new GetByIdPharamcyCommand(Id)));
        }

        [HttpPut("Update")]

        public async Task<IActionResult> UpdateAsync(int Id , PharmacyDTO pharmacyDTO) 
        {
            return Ok(await _sender.Send(new UpdatePharmacyCommand(Id,pharmacyDTO)));
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteAsync(int Id) 
        {
            return Ok(await _sender.Send(new DeletePharmacyCommand(Id)));
        }
    }
}




