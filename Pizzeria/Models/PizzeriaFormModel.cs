namespace Pizzeria.Models

{
    public class PizzeriaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category> Categories { get; set; }

        public PizzeriaFormModel() { }

        public PizzeriaFormModel (Pizza pizza, List<Category> categories)
        {
            this.Pizza = pizza;
            this.Categories = categories;
        }
    }
}
