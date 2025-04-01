using Wafra.Core.Entites;
using Wafra.Core.Interfaces;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class MedicinRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicinRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
