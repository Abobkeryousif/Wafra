
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Entites;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class VerificationRepository : GenericRepository<Verification>, IVerificationRepository
    {
        public VerificationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
