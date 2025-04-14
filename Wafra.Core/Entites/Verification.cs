
using Wafra.Core.Common.Enum;

namespace Wafra.Core.Entites
{
    public class Verification
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime ExpierOn { get; set; }
        public bool IsExpier => DateTime.Now > ExpierOn;
        public TokenPerpoues TokenPerpoues { get; set; }
    }
}
