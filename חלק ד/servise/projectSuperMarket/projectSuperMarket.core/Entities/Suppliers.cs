namespace projectSuperMarket.Entities
{
    public class Suppliers
    {
        public int Id {  get; set; }
        public string CompanyName {  get; set; }
        public string PhoneNumber { get; set; }
        public string RepresentativeName { get; set; }
        public List<Goods> Goods { get; set; }

    }
}
