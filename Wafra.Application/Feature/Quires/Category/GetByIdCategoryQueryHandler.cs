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

namespace Wafra.Application.Feature.Quires.Category
{


    public record GetByIdCategoryCommand(int Id) : IRequest<HttpResult<CategoryDTO>>;
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryCommand, HttpResult<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResult<CategoryDTO>> Handle(GetByIdCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (result is null) 
                return new HttpResult<CategoryDTO>(HttpStatusCode.NotFound,$"Not Found With ID:{request.Id}");
            var categoryDto = new CategoryDTO { Name = result.CategoryName };
            return new HttpResult<CategoryDTO>(HttpStatusCode.OK,"Sccuess",categoryDto);
                
        }
    }
}
