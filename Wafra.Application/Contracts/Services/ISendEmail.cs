using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wafra.Application.Contracts.Services
{
    public interface ISendEmail
    {
        void SendEmail(string mailTo,string subject, string message);
    }
}
