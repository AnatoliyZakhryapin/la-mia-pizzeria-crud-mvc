using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Models
{
    public class PizzeriaDatabaseContext : DbContext 
    {

        public DbSet<Pizza> Pizza { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Pizzeria;Integrated Security=True;Pooling=False;Encrypt=False;TrustServerCertificate=False");
        }
    }
}
