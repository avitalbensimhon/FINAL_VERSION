using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Servies
{
    public interface ISupplersServies
    {
        List<Suppliers> GetAll();
        Suppliers GetById(int id);
        void Add(Suppliers supplier);
        void Update(Suppliers supplier);
        void Delete(int id);
    }
}
