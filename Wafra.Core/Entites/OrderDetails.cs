using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Entites
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; }

        public DateTime OrderDate => DateTime.Now;
    }
}
