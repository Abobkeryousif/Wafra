namespace Wafra.Core.Entites
{
    public class  Medicines
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public List<Pharmacies> Pharmacies { get; set; }

        public Category Category { get; set; }

        public List<PharmacyMedicine> pharmacyMedicines { get; set; }   
    }
}
