using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Repository
{
    public interface IOrderRepository
    {
        List<Orders> GetAll();
        Orders GetById(int id);
        Task AddOrderAsync(Orders order);
        void Update(Orders order);
        void Delete(int id);
        void UpdateStatus(int id, StatusOrder newStatus);
    }
}
