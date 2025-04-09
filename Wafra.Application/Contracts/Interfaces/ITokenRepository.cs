using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafra.Core.Common;
using Wafra.Core.Entites;

namespace Wafra.Application.Contracts.Interfaces
{
    public interface ITokenRepository : IGenericRepository<RefreshToken>
    {
        string CreateToken(Users users);
        RefreshToken GenerateRefreshToken();
        
    }
}
