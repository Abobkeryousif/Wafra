using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Entites;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
