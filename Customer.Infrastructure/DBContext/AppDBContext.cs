using Customer.Core.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
//using Customer.Core.DomainModels;
namespace Customer.Infrastructure.DBContext
{
    public class AppDBContext : DbContext
    {
        private HttpContext _httpContext { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Customer.Core.DomainModels.Client> Clients { get; set; }
        public DbSet<Customer.Core.DomainModels.StockMarket> StockMarkets { get; set; }
        protected async override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // add your connection string and create this DB 
            optionsBuilder.UseSqlServer("Server =.; Database = ClientDB; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            }); 

        }
    }
}