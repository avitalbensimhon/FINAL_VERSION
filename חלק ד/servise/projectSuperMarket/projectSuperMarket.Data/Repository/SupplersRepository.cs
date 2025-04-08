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
    public class SupplersRepository: ISupplersRepository
    {
        private readonly DataContext _context;
        public SupplersRepository(DataContext context)
        {
            _context = context;
        }
        public List<Suppliers> GetAll()
        {
            return _context.SuppliersList.Include(o => o.Goods).ToList();
        }

        public Suppliers GetById(int id)
        {
            var Item= _context.SuppliersList.FirstOrDefault(s => s.Id == id);
            if (Item == null)
                return null;
            return Item;
        }

        public void Add(Suppliers supplier)
        {
            _context.SuppliersList.Add(supplier);
            _context.SaveChanges();
        }

        public void Update(Suppliers supplier)
        {
            _context.SuppliersList.Update(supplier);
            _context.SaveChanges();
        }
        public Suppliers GetByPhone(string phoneNumber)
        {
            return _context.SuppliersList.FirstOrDefault(s => s.PhoneNumber == phoneNumber);
        }
        public void Delete(int id)
        {
            var supplier = _context.SuppliersList.FirstOrDefault(s => s.Id == id);
            if (supplier != null)
            {
                _context.SuppliersList.Remove(supplier);
                _context.SaveChanges();
            }
        }
    }
}
