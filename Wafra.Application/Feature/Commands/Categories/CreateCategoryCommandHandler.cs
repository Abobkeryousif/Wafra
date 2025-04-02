using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Category;
using Wafra.Core.Common;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.Commands.Categories
{
    public record CreateCategoryCommand(CategoryDTO Category) : IRequest<HttpResult<string>>;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, HttpResult<string>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResult<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _categoryRepository.IsExist(c => c.CategoryName == request.Category.Name);
            if (isExist)
            {
                return new HttpResult<string>(HttpStatusCode.BadRequest, "This Category Alreday Exisy");
            }

            var category = new Category
            {
                CategoryName = request.Category.Name,
            };

            await _categoryRepository.CreateAsync(category);
            return new HttpResult<string>(HttpStatusCode.OK, "Success", $"Category with Name : {category.CategoryName} is Added");
        }
    }
}
