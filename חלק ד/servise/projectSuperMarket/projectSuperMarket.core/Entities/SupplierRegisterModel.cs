using projectSuperMarket.Entities;
using projectSuperMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Entities
{
    public class SupplierRegisterModel
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string RepresentativeName { get; set; }
        public List<GoodsPostModel> Goods { get; set; }
    }
}
