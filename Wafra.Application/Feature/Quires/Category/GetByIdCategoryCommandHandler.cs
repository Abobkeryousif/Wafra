using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Feature.DTOs.Category;
using Wafra.Application.Observability.CustomMetrics;
using Wafra.Application.Observability.CustomTraces;
using Wafra.Application.Observability.Helper;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Quires.Category
{
    public record GetByIdCategoryCommand(int Id) : IRequest<HttpResult<CategoryDTO>>;
    public class GetByIdCategoryCommandHandler : IRequestHandler<GetByIdCategoryCommand, HttpResult<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetByIdCategoryCommandHandler> _logger;

        public GetByIdCategoryCommandHandler(ILogger<GetByIdCategoryCommandHandler> logger ,ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<HttpResult<CategoryDTO>> Handle(GetByIdCategoryCommand request, CancellationToken cancellationToken)
        {

            const string module = "category";
            const string operation = "get_by_id";


            using var activity = CategoryActivitySource.Source.StartActivity("categort.get_by_id");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var result = await _categoryRepository.FirstOrDefaultAsync(x=> x.Id == request.Id);

            var status = result is null ? "not_found" : "success";

            if (result is null)
            {
                _logger.LogWarning("category not found with ID: {CategoryId}", request.Id);

                activity?.SetTag("wafra.category.outcome", status);
                BusinessMetricsHelper.RecordOperation(module,operation, "failure");

                return new HttpResult<CategoryDTO>(HttpStatusCode.NotFound, $"Not Found With ID:{request.Id}");
            }

            var categoryDto = new CategoryDTO { Name = result.CategoryName };
            stopwatch.Stop();

            _logger.LogInformation("category {CategoryName} retrieved successfly", result.CategoryName);

            activity?.SetTag("wafra.category.outcome", "success");
       

            BusinessMetricsHelper.RecordOperation(module, operation, "success");
            BusinessMetricsHelper.RecordDuration(module, operation, stopwatch.ElapsedMilliseconds);

            return new HttpResult<CategoryDTO>(HttpStatusCode.OK,"Sccuess",categoryDto);
                
        }
    }
}
