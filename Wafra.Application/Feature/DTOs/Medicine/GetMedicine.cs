﻿

namespace Wafra.Application.Feature.DTOs.Medicin
{
    public class GetMedicine
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
