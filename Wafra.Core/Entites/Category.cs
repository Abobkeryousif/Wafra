﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Core.Entites
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Medicine> Medicines { get; set; }   
    }
}
