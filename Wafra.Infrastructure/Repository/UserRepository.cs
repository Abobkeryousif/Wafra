using Wafra.Core.Entites;
using Wafra.Core.Interfaces;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
