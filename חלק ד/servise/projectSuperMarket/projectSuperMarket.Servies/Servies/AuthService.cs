
using Microsoft.EntityFrameworkCore;
using projectSuperMarket.core.Entities;
using projectSuperMarket.core.Repository;
using projectSuperMarket.Entities;
using projectSuperMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSuperMarket.Servies.Servies
{
    public  class AuthService
    {
        private readonly ISupplersRepository _suppliersRepo;
        private readonly JwtTokenService _jwtService;
        private readonly DataContext _context;
        public AuthService(ISupplersRepository repo, JwtTokenService jwtService, DataContext context)
        {
            _suppliersRepo = repo;
            _jwtService = jwtService;
            _context = context;
        }

        public async Task<Suppliers> RegisterAsync(SupplierRegisterModel model)
        {
            try
            {
                var supplier = new Suppliers
            {
                CompanyName = model.CompanyName,
                PhoneNumber = model.PhoneNumber,
                RepresentativeName = model.RepresentativeName,
            };

            _context.SuppliersList.Add(supplier);
            await _context.SaveChangesAsync();


            var goods = model.Goods.Select(g => new Goods
            {
                ProductName = g.ProductName,
                PricePerItem = g.PricePerItem,
                MinQuantity = g.MinQuantity,
                SupplierId = supplier.Id

            }).ToList();
            _context.GoodsList.AddRange(goods);
            await _context.SaveChangesAsync();

            return supplier;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; 
            }
        }
        public async Task<string> LoginAsync(SupplierLoginModel model)
        {
            if (model.Name == "admin" && model.Password == "admin123")
            {
          
                return _jwtService.GenerateToken(null, model.Name, model.Password);
            }

          
            var supplier = await _context.SuppliersList
                .FirstOrDefaultAsync(s => s.CompanyName == model.Name && s.PhoneNumber == model.Password);

            if (supplier == null)
                return null;

          
            return _jwtService.GenerateToken(supplier, null, null);
        }

    }
}
