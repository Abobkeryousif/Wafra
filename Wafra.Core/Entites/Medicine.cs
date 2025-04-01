﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Entites
{
    public class  Medicine
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public List<Pharmacy> Pharmacies { get; set; }

        public Category Category { get; set; }

        public List<PharmacyMedicine> pharmacyMedicines { get; set; }   
    }
}
