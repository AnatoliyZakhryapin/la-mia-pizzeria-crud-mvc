namespace Pizzeria.Models
{
    public class Pizza
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FotoUrl { get; set; }
        public decimal Price { get; set; }

        public Pizza( string name, string description, string fotoUrl, decimal price)
        {
            Name = name;
            Description = description;
            FotoUrl = fotoUrl;
            Price = price;
        }
    }
}
