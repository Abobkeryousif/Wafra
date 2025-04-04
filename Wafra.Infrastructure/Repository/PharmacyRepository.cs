using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class PharmacyRepository : GenericRepository<Pharmacies> , IPharamcyRepository
    {
        public PharmacyRepository(ApplicationDbContext context) : base(context) 
        {
        }
    }
}
