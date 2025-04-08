using projectSuperMarket.core.Repository;
using projectSuperMarket.core.Servies;
using projectSuperMarket.Data.Repository;
using projectSuperMarket.Entities;
using projectSuperMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.Servies.Servies
{
    public class OrderServies:IOrderServies
    {
        private readonly IOrderRepository _IorderRepository;
        public OrderServies(IOrderRepository orderRepository)
        {
           _IorderRepository = orderRepository;
        }
        public List<Orders> GetAll() => _IorderRepository.GetAll();

        public Orders GetById(int id) => _IorderRepository.GetById(id);

        public async Task AddOrderAsync(OrderPostModel model)
        {
            var order = new Orders
            {
                SupplierId = model.SupplierId,
                QuantityOrdered = model.QuantityOrdered,
                
                  StatusOrders = 0,
                Goods = model.Goods.Select(g => new Goods
                {
                    ProductName = g.ProductName,
                    PricePerItem = g.PricePerItem,
                     MinQuantity = g.MinQuantity,
                    SupplierId = model.SupplierId
                }).ToList()
            };

            await _IorderRepository.AddOrderAsync(order);
        }

        public void Update(Orders order) => _IorderRepository.Update(order);

        public void Delete(int id) => _IorderRepository.Delete(id);

        public void UpdateStatus(int id, StatusOrder newStatus) => _IorderRepository.UpdateStatus(id, newStatus);
    }
}
