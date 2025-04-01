using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Entites
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public List<Medicine> Medicines { get; set; }  

        public List<PharmacyMedicine> pharmacyMedicines { get; set; }

        public List<Order> Orders { get; set; }  
    }
}
