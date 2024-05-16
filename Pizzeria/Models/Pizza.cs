using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    [Table("pizzas")]
    [Index(nameof(Name), IsUnique = true)]
    public class Pizza
    {
        public long PizzaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FotoUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public Pizza() { }
        public Pizza(string name, string description, string fotoUrl, decimal price)
        {
            Name = name;
            Description = description;
            FotoUrl = fotoUrl;
            Price = price;
        }
    }
}
