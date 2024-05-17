using Pizzeria.Models;

namespace Pizzeria.Data
{
    public static class PizzaManager
    {
        public static void PizzaSeeder()
        {
            if (CountAllPizzas() == 0)
            {
                try
                {
                    PizzaManager.AddNewPizza(new Pizza("Margherita", "Pola di pomodoro, Fiordilatte, Origano, Basilico", "~/img/pizza_margherita.png", 7.99m));
                    PizzaManager.AddNewPizza(new Pizza("Prosciutto e mozzarella", "Pola di pomodoro, Fiordilatte, Prosciutto", "~/img/pizza_prosciutto.png", 9.99m));
                    PizzaManager.AddNewPizza(new Pizza("Divola", "Pola di pomodoro, Fiordilatte, Salame", "~/img/pizza_diavola.png", 10.99m));
                    PizzaManager.AddNewPizza(new Pizza("Quattro formaggi", "Mozzarella, Gorgonzola, Fontina, Parmigiano, Polpa di pomodoro", "~/img/pizza_quattro_formaggi.png", 11.99m));
                    PizzaManager.AddNewPizza(new Pizza("Napoletana", "Mozzarella, pomodoro, acciughe, origano, olio d’oliva Polpa di pomodoro", "~/img/pizza_napoletana.png", 10.99m));
                }
                catch (Exception) { }
            }
        }

        public static int CountAllPizzas()
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.Count();
        }

        public static void AddNewPizza(Pizza pizza)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            db.Add(pizza);
            db.SaveChanges();
        }

        public static List<Pizza> GetAllPizzas()
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.ToList();
        }

        public static Pizza GetPizzaByName(string name)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.FirstOrDefault(p => p.Name == name);
        }

        public static Pizza GetPizzaById(int id)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.FirstOrDefault(p => p.PizzaId == id);
        }

        public static bool UpdatePizza(long id, Pizza pizzaUpdated)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            var pizzaToUpdate = db.Pizzas.Find(id);

            if (pizzaToUpdate != null)
            {
                pizzaToUpdate.Name = pizzaUpdated.Name;
                pizzaToUpdate.Description = pizzaUpdated.Description;
                pizzaToUpdate.Price = pizzaUpdated.Price;
                pizzaToUpdate.FotoUrl = pizzaToUpdate.FotoUrl;

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
