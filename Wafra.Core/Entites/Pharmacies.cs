namespace Wafra.Core.Entites
{
    public class Pharmacies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public string location { get; set; }

        public List<Medicines> Medicines { get; set; }  

        public List<PharmacyMedicine> pharmacyMedicines { get; set; }

        public List<Order> Orders { get; set; }  
    }
}
