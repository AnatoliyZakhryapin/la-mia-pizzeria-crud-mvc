using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Pizzeria.Data.CustomValidationeRules;

namespace Pizzeria.Models
{
    [Table("pizzas")]
    [Index(nameof(Name), IsUnique = true)]
    public class Pizza
    {
        public long PizzaId { get; set; }

        [Required(ErrorMessage = "Il nome della pizza è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non deve avere al più 50 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione della pizza è obbligatoria.")]
        [MinWords(5)]
        public string Description { get; set; }

        public string? FotoUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [EmptyInput()]
        //[Required(ErrorMessage = "Il prezzo della pizza è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero.")]
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
