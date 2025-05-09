﻿
namespace Wafra.Core.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int PharmacyID { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public Users Users { get; set; }
        public Decimal TotalPrice { get; set; }
        public Pharmacies Pharmacy { get; set; }
        public DateTime OrderDate => DateTime.Now;
    }
}
