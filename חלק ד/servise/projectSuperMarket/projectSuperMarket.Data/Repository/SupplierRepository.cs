using Microsoft.EntityFrameworkCore;
using projectSuperMarket.core.Repository;
using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.Data.Repository
{
    public class SupplierRepository:ISupplierRepository
    {
        private readonly DataContext _context;

        public SupplierRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Suppliers> GetCheapestSupplierForProduct(string productName)
        {
            var suppliers = await _context.SuppliersList
                .Include(s => s.Goods)
                .Where(s => s.Goods.Any(g => g.ProductName == productName))
                .ToListAsync();

            var cheapestSupplier = suppliers
                .Select(s => new
                {
                    Supplier = s,
                    CheapestPrice = s.Goods
                        .Where(g => g.ProductName == productName)
                        .Min(g => g.PricePerItem)
                })
                .OrderBy(x => x.CheapestPrice)
                .FirstOrDefault();

            return cheapestSupplier?.Supplier;
        }
    }
}
