using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.CustomMetrics;
using Wafra.Application.CustomTraces;
using Wafra.Application.Feature.DTOs.Category;
using Wafra.Core.Common;



namespace Wafra.Application.Feature.Quires.Category
{
    public record GetCategoriesCommand : IRequest<HttpResult<List<GetCategory>>>;
    public class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, HttpResult<List<GetCategory>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoriesCommandHandler> _logger;

        public GetCategoriesCommandHandler(ILogger<GetCategoriesCommandHandler> logger ,ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<HttpResult<List<GetCategory>>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
        {
            using var activity = CategoryActivitySource.Source.StartActivity("categoriea.get_all");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var result = await _categoryRepository.GetALLAsync();
            

            if (result.Count == 0)
            {
                _logger.LogWarning("not found category in db");

                activity?.SetTag("wafra.category.outcome", "not_found"); //outcome powerful because help us in traces filtering 
                

                return new HttpResult<List<GetCategory>>(HttpStatusCode.NotFound, "Category Not Found");
            }

            var categories = result.Select(c => new GetCategory { Name = c.CategoryName , Id = c.Id }).ToList();

            _logger.LogInformation("successfly retrived {CategoryCount} categories.", categories.Count);

            activity?.SetTag("wafra.category.outcome", "success");
            activity?.SetTag("wafra.category.result_count", categories.Count);
            activity?.SetStatus(ActivityStatusCode.Ok);

            stopwatch.Stop();

            CategoryMetrics.GetAllCategoreis.Add(1);
            CategoryMetrics.GetAllCategoryDuration.Record(stopwatch.ElapsedMilliseconds);


            return new HttpResult<List<GetCategory>>(HttpStatusCode.OK, "All Category Has Returned",categories);
        }
    }
}
