using Microsoft.EntityFrameworkCore;
using projectSuperMarket.core.Repository;
using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.Servies.Servies
{
    public class AutoOrderService
    {
        private readonly ISupplierRepository _supplierRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly DataContext _context;

        public AutoOrderService(ISupplierRepository supplierRepo, IOrderRepository orderRepo, DataContext context)
        {
            _supplierRepo = supplierRepo;
            _orderRepo = orderRepo;
            _context = context;
        }
        public async Task<object> CheckAndOrderIfNeeded(Dictionary<string, int> soldItems)
        {
            var messages = new List<string>();
            var createdOrders = new List<Orders>();

            foreach (var item in soldItems)
            {
                string productName = item.Key;
                int quantitySold = item.Value;

                int currentStock = await GetStockForProduct(productName);
                int minRequired = 5; 

              
                if (quantitySold > currentStock)
                {
                    messages.Add($"❌ אין מספיק מלאי למוצר '{productName}'. קיים רק {currentStock}, ביקשת למכור {quantitySold}.");
                    continue;
                }

               
                int stockAfterSale = currentStock - quantitySold;

                if (stockAfterSale > minRequired)
                {
                    var cheapestSupplier = await _supplierRepo.GetCheapestSupplierForProduct(productName);

                    if (cheapestSupplier != null)
                    {
                        var newOrder = new Orders
                        {
                            SupplierId = cheapestSupplier.Id,
                            QuantityOrdered = 10,
                            StatusOrders = 0, 
                            Goods = new List<Goods>
                    {
                        new Goods
                        {
                            ProductName = productName,
                            PricePerItem = cheapestSupplier.Goods.First(g => g.ProductName == productName).PricePerItem,
                            MinQuantity = 1,
                            SupplierId = cheapestSupplier.Id
                        }
                    }
                        };

                        
                        await _orderRepo.AddOrderAsync(newOrder);
                        createdOrders.Add(newOrder);
                        messages.Add($"✅ הזמנה אוטומטית נשלחה לספק עבור '{productName}'.");
                    }
                    else
                    {
                        messages.Add($"⚠ אין ספק מתאים למוצר '{productName}', יש להזמין ידנית.");
                    }
                }
                else
                {
                   
                    messages.Add($"ℹ המלאי של '{productName}' לאחר המכירה עדיין מעל המינימום – לא בוצעה הזמנה.");
                }
            }

            return new
            {
                orders = createdOrders,
                messages = messages
            };
        }




        private async Task<int> GetStockForProduct(string productName)
        {
            var normalizedName = productName.ToLower().Trim();
            Console.WriteLine($"🔎 מחפשים מלאי למוצר: '{normalizedName}'");

            var list = await _context.GoodsList
                .Where(g => g.ProductName.ToLower().Trim() == normalizedName)
                .ToListAsync();

            Console.WriteLine($"📦 נמצאו {list.Count} מופעים של המוצר");

            foreach (var item in list)
            {
                Console.WriteLine($"➡️ {item.ProductName}, כמות: {item.MinQuantity}");
            }

            return list.Sum(g => g.MinQuantity);
        }
    }
}
