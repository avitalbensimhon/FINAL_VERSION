using System.ComponentModel.DataAnnotations;

namespace projectSuperMarket.Entities
{
   public enum StatusOrder
    {
        [Display(Name = "ממתין")]
        Waiting,
        [Display(Name = "בטיפול")]
        Processing,
        [Display(Name = "הושלמה")]
        Completed
    }
    public class Orders
    {
        public int Id {  get; set; }
        public int SupplierId { get; set; }
        public List<Goods> Goods { get; set; }
        public int QuantityOrdered { get; set; }
        public StatusOrder StatusOrders { get; set; }
    }
}
