using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class PizzeriaController : Controller
    {
        public PizzeriaDatabaseContext DatabaseContext = new PizzeriaDatabaseContext();
        public IActionResult Index()
        {

            if (DatabaseContext.Pizzas.Count() == 0)
            {
                List<Pizza> listaPizze = new List<Pizza>();

                listaPizze.Add(new Pizza("Margherita", "Pola di pomodoro, Fiordilatte, Origano, Basilico", "~/img/pizza_margherita.png", 7.99m));
                listaPizze.Add(new Pizza("Prosciutto e mozzarella", "Pola di pomodoro, Fiordilatte, Prosciutto", "~/img/pizza_prosciutto.png", 9.99m));
                listaPizze.Add(new Pizza("Divola", "Pola di pomodoro, Fiordilatte, Salame", "~/img/pizza_diavola.png", 10.99m));
                listaPizze.Add(new Pizza("Quattro formaggi", "Mozzarella, Gorgonzola, Fontina, Parmigiano, Polpa di pomodoro", "~/img/pizza_quattro_formaggi.png", 11.99m));
                listaPizze.Add(new Pizza("Napoletana", "Mozzarella, pomodoro, acciughe, origano, olio d’oliva Polpa di pomodoro", "~/img/pizza_napoletana.png", 10.99m));

                try
                {
                    DatabaseContext.AddRange(listaPizze);
                    DatabaseContext.SaveChanges();
                }
                catch (Exception) { }
            }

            List<Pizza> listaPizzas = DatabaseContext.Pizzas.ToList();

            return View(listaPizzas);
        }
        [Route("/Pizzeria/{name}")]
        public IActionResult Show(int id)
        {

            Pizza pizza = DatabaseContext.Pizzas.Where(x => x.PizzaId == id).FirstOrDefault();

            return View(pizza);
        }
    }
}
