using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Models
{
    public class GoodsPostModelOrders
    {
   
        public string ProductName { get; set; }
        public float PricePerItem { get; set; }
        public int MinQuantity { get; set; }
        public int SupplierId { get; set; }
    }
}
