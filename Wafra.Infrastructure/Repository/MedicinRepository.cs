using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Entites;

using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class MedicinRepository : GenericRepository<Medicines> , IMedicineRepository
    {
        public MedicinRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
