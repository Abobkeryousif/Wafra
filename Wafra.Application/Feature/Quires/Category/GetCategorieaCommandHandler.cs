using MediatR;
using System;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Category;
using Wafra.Core.Common;
using Wafra.Core.Entites;


namespace Wafra.Application.Feature.Quires.Category
{
    public record GetCategorieaCommand : IRequest<HttpResult<List<CategoryDTO>>>;
    public class GetCategorieaCommandHandler : IRequestHandler<GetCategorieaCommand, HttpResult<List<CategoryDTO>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategorieaCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResult<List<CategoryDTO>>> Handle(GetCategorieaCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetALLAsync();
            if (result.Count == 0) 
                return new HttpResult<List<CategoryDTO>>(HttpStatusCode.NotFound,"Category Not Found");

            var categories = result.Select(c => new CategoryDTO { Name = c.CategoryName }).ToList();
            return new HttpResult<List<CategoryDTO>>(HttpStatusCode.OK, "All Category Has Returned",categories);
        }
    }
}
