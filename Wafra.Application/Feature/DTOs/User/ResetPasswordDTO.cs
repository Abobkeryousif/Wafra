
using System.ComponentModel.DataAnnotations;

namespace Wafra.Application.Feature.DTOs.User
{
    public class ResetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string token { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confiermPassword { get; set; }
    }
}
