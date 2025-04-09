using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Common
{
    public class AuthResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime ExpierdOn { get; set; }
    }
}
