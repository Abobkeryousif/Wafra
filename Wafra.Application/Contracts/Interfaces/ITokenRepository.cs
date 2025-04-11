using Wafra.Core.Entites;

namespace Wafra.Application.Contracts.Interfaces
{
    public interface ITokenRepository 
    {
        string CreateToken(Users users);
        RefreshToken GenerateRefreshToken();
        
    }
}
