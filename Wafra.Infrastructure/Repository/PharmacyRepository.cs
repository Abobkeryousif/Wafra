using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class PharmacyRepository : GenericRepository<Pharmacy>
    {
        public PharmacyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
