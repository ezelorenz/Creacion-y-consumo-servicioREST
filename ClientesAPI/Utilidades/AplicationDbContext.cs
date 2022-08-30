using ClientesAPI.Entidades;
using Microsoft.EntityFrameworkCore;
namespace ClientesAPI.Utilidades
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
