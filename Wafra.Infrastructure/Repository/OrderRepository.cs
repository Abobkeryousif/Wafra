using Wafra.Core.Entites;
using Wafra.Core.Interfaces;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
