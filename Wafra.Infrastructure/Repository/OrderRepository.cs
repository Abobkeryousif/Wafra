using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
