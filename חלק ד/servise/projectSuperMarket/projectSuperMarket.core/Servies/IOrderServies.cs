using projectSuperMarket.Entities;
using projectSuperMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Servies
{
    public interface IOrderServies
    {
        List<Orders> GetAll();
        Orders GetById(int id);
        Task AddOrderAsync(OrderPostModel model);
        void Update(Orders order);
        void Delete(int id);
        void UpdateStatus(int id, StatusOrder newStatus);
    }
}
