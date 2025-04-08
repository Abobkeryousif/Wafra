using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Entites
{
    public class OTP
    {
        public Guid Id { get; set; }
        public string Otp { get; set; }
        public string UserEmail { get; set; }
        public bool IsUsed { get; set; } 
        public DateTime ExpriationOn { get; set; }
        public bool IsExpired => DateTime.Now > ExpriationOn;
    }
}
