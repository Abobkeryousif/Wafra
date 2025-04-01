using Wafra.Core.Entites;
using Wafra.Core.Interfaces;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
