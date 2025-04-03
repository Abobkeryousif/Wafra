using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails>
    {
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
