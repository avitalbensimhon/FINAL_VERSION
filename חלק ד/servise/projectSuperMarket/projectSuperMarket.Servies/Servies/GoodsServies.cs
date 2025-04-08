using projectSuperMarket.core.Repository;
using projectSuperMarket.core.Servies;
using projectSuperMarket.Data.Repository;
using projectSuperMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.Servies.Servies
{
    public class GoodsServies:IGoodsServies
    {
        private readonly IGoodsRepository _IgoodsRepository;
        public GoodsServies(IGoodsRepository IgoodsRepository)
        {
            _IgoodsRepository = IgoodsRepository;
        }
        public List<Goods> GetAll()
        {
            return _IgoodsRepository.GetAll();
        }
        public Goods GetGoodsById(int id)
        {
            return _IgoodsRepository.GetGoodsById(id);
        }
        public void Add(Goods item)
        {
            _IgoodsRepository.Add(item);
        }
        public void Update(Goods goods)
        {
            _IgoodsRepository.Update(goods);
        }
        public void Delete(int id)
        {
            _IgoodsRepository.Delete(id);
        }

    }
}
