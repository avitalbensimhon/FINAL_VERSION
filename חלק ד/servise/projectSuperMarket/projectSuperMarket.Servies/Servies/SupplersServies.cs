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
    public class SupplersServies:ISupplersServies
    {
        private readonly ISupplersRepository _IsupplersRepository;

        public SupplersServies(ISupplersRepository IsupplersRepository)
        {
            _IsupplersRepository = IsupplersRepository;
        }

        public List<Suppliers> GetAll() => _IsupplersRepository.GetAll();
        public Suppliers GetById(int id) => _IsupplersRepository.GetById(id);
        public void Add(Suppliers supplier) => _IsupplersRepository.Add(supplier);
        public void Update(Suppliers supplier) => _IsupplersRepository.Update(supplier);
        public void Delete(int id) => _IsupplersRepository.Delete(id);


    }
}
