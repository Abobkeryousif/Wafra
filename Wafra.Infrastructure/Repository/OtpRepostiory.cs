
using Wafra.Application.Contracts.Interfaces;
using Wafra.Core.Entites;
using Wafra.Infrastructure.Data;

namespace Wafra.Infrastructure.Repository
{
    public class OtpRepostiory : GenericRepository<OTP>, IOtpRepository
    {
        public OtpRepostiory(ApplicationDbContext context) : base(context)
        {
        }
    }
}
