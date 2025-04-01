using Wafra.Core.Entites;
using Wafra.Core.Interfaces;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class PharmacyRepository : GenericRepository<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
