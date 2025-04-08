namespace projectSuperMarket.Entities
{
    public class Goods
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float PricePerItem { get; set; }
        public int MinQuantity { get; set; }
        
        public int SupplierId { get; set; }
        public Suppliers Supplier { get; set; }
        
    }
}
