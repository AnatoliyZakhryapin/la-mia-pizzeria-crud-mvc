using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pizzeria.Models;

namespace Pizzeria.Data
{
    public class PizzeriaDatabaseContext : DbContext
    {

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Pizzeria;Integrated Security=True;Pooling=False;Encrypt=False;TrustServerCertificate=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .HasOne(o => o.Category)
                .WithMany(o => o.Pizzas)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
