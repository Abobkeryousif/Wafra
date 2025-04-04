using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Entites
{
    public class PharmacyMedicine
    {
        public int Id { get; set; }

        public int PharmacyId  { get; set; }
        public Pharmacies pharmacy { get; set; }

        public int MedicineId { get; set; }

        public Medicines Medicine { get; set; }

        public int Stock { get; set; }
    }
}
