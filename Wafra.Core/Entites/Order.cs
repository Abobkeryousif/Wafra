
namespace Wafra.Core.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int PharmacyID { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public User Users { get; set; }
        public Decimal TotalPrice { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public DateTime OrderDate => DateTime.UtcNow;
    }
}
