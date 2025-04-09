
namespace Wafra.Core.Entites
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public bool IsValid { get; set; } 

        public List<RefreshToken> refreshTokens { get; set; } = new List<RefreshToken>(); 

    }
}
