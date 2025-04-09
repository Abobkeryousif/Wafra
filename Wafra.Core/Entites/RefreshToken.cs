
namespace Wafra.Core.Entites
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }

        public DateTime ExpierOn { get; set; }

        public bool IsExiperd => DateTime.UtcNow >= ExpierOn;

        public DateTime CreatedOn { get; set; }
        public DateTime? RevokeOn { get; set; }

        public bool IsActive => RevokeOn == null && !IsExiperd;
    }
}
