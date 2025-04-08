using projectSuperMarket.core.Models;
using projectSuperMarket.Entities;

namespace projectSuperMarket.Models
{
    public class OrderPostModel
    {
        public int SupplierId { get; set; }
        public int QuantityOrdered { get; set; }
        public StatusOrder StatusOrders { get; set; }
        public List<GoodsPostModel> Goods { get; set; }
    }
}
