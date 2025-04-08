using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.core.Servies
{
    public interface IGoodsServies
    {
        List<Goods> GetAll();
        Goods GetGoodsById(int id);
        void Add(Goods item);
        void Update(Goods goods);
        void Delete(int id);
    }
}
