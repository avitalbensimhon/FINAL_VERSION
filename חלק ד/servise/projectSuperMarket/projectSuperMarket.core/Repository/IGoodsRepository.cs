using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Repository
{
    public interface IGoodsRepository
    {
        List<Goods> GetAll();
        void Add(Goods good);
        void Update(Goods goods);
        void Delete(int Id);
        Goods GetGoodsById(int Id);
    }
}
