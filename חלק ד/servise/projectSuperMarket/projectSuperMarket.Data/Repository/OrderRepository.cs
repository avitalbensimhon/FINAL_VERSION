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
    public class OrderRepository: IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public List<Orders> GetAll() => _context.OrdersList
            .Include(o => o.Goods) 
            .ToList();

        public Orders GetById(int id) => _context.OrdersList
            .Include(o => o.Goods)
            .FirstOrDefault(o => o.Id == id);

        public async Task AddOrderAsync(Orders order)
        {
            await _context.OrdersList.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public void Update(Orders order)
        {
            var existing = _context.OrdersList
                .Include(o => o.Goods)
                .FirstOrDefault(o => o.Id == order.Id);

            if (existing != null)
            {
                existing.SupplierId = order.SupplierId;
                existing.Goods = order.Goods;
                existing.QuantityOrdered = order.QuantityOrdered;
                existing.StatusOrders = order.StatusOrders;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var order = _context.OrdersList.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _context.OrdersList.Remove(order);
                _context.SaveChanges();
            }
        }

        public void UpdateStatus(int id, StatusOrder newStatus)
        {
            var order = _context.OrdersList.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.StatusOrders = newStatus;
                _context.SaveChanges();
            }
        }


    }
}
