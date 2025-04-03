using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Common;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Commands.Categories
{
    public record DeleteCategoryCommand(int Id) : IRequest<HttpResult<string>>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, HttpResult<string>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResult<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
                if(result == null) 
                  return new HttpResult<string>(HttpStatusCode.NotFound , $"Not Found With ID: {request.Id}");
                var Category = new Category { CategoryName = result.CategoryName };
            await _categoryRepository.DeleteAsync(Category);
            return new HttpResult<string>(HttpStatusCode.NotFound, "Delete Complete!");
        }
    }
}
