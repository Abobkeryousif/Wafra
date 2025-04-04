using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Application.Feature.DTOs.Pharmacy
{
    public class GetPharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public string location { get; set; }
    }
}
