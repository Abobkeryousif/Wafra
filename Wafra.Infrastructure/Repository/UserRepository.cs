using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
