﻿using AutoMapper;
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
    public record UpdateCategoryCommand(int Id , CategoryDTO CategoryDTO) : IRequest<HttpResult<CategoryDTO>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, HttpResult<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository , IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            
        }

        public async Task<HttpResult<CategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (result == null) 
                return new HttpResult<CategoryDTO>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");
            var Category = new Category { CategoryName = result.CategoryName };
            await _categoryRepository.UpdateAsync(Category);
            return new HttpResult<CategoryDTO>(HttpStatusCode.OK,"Category Updated Sccussefly!");
        }
    }
}
