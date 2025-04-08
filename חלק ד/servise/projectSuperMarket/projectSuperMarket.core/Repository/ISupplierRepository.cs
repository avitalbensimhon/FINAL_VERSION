using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Repository
{
    public interface ISupplierRepository
    {
        Task<Suppliers> GetCheapestSupplierForProduct(string productName);
    }
}
