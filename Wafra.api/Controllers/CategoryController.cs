using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wafra.Application.Feature.Commands.Categories;
using Wafra.Application.Feature.DTOs.Category;
using Wafra.Application.Feature.Quires.Category;

namespace Wafra.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CategoryDTO categoryDTO) 
        {
            var result = await _sender.Send(new CreateCategoryCommand(categoryDTO));
            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync() 
        {
            var result = await _sender.Send(new GetCategorieaCommand());
            return Ok(result);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int Id) 
        {
            var result = await _sender.Send(new GetByIdCategoryCommand(Id));
            return Ok(result);
        }
        [HttpPut("Update")]

        public async Task<IActionResult> UpdateAsync([FromQuery]int Id , CategoryDTO categoryDTO) 
        {
            var result = await _sender.Send(new UpdateCategoryCommand(Id,categoryDTO));
            return Ok(result);
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteAsync(int Id) 
        {
            var result = await _sender.Send(new DeleteCategoryCommand(Id));
            return Ok(result);
        }


    }
}










