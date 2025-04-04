using MediatR;
using System;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Category;
using Wafra.Core.Common;
using Wafra.Core.Entites;


namespace Wafra.Application.Feature.Quires.Category
{
    public record GetCategorieaCommand : IRequest<HttpResult<List<GetCategory>>>;
    public class GetCategorieaCommandHandler : IRequestHandler<GetCategorieaCommand, HttpResult<List<GetCategory>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategorieaCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResult<List<GetCategory>>> Handle(GetCategorieaCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetALLAsync();
            if (result.Count == 0) 
                return new HttpResult<List<GetCategory>>(HttpStatusCode.NotFound,"Category Not Found");

            var categories = result.Select(c => new GetCategory { Name = c.CategoryName , Id = c.Id }).ToList();
            return new HttpResult<List<GetCategory>>(HttpStatusCode.OK, "All Category Has Returned",categories);
        }
    }
}
