using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafra.Core.Entites;

namespace Wafra.Application.Contracts.Services
{
    public interface ITokenRepository
    {
        string CreateToken(Users users);
    }
}
