using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using projectSuperMarket.Entities;

namespace projectSuperMarket
{
    public class DataContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Goods> GoodsList{ get; set; }
        public DbSet<Orders> OrdersList {  get; set; }
        public DbSet<Suppliers> SuppliersList {  get; set; }

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
        }

    }
}
