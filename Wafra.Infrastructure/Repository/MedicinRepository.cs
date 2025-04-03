using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class MedicinRepository : GenericRepository<Medicine>
    {
        public MedicinRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
