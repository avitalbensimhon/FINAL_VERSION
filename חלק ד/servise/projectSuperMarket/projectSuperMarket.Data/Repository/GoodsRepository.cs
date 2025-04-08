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
    
    public class GoodsRepository:IGoodsRepository
    {
        private readonly DataContext _context;
        public GoodsRepository(DataContext context)
        {
            _context = context;
        }
        public List<Goods> GetAll()
        {
            return _context.GoodsList.ToList();
        }
        public Goods GetGoodsById(int Id)
        {
            var item= _context.GoodsList.FirstOrDefault(o=>o.Id==Id);
            if (item==null)
                return null;
            return item;
        }
        public void Add(Goods good)
        {
            _context.GoodsList.Add(good);
            _context.SaveChanges();
        }
        public void Update(Goods goods)
        {
            _context.GoodsList.Update(goods);
        }
        public void Delete(int Id)
        {
            var g = _context.GoodsList.FirstOrDefault(o => o.Id == Id);  
            if(g==null) return;
            _context.GoodsList.Remove(g);
            _context.SaveChanges();
        }
    }
}
