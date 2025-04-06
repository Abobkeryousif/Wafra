using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafra.Core.Entites;

namespace Wafra.Application.Feature.DTOs.User
{
    public class GetUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValid { get; set; }
    }
}
