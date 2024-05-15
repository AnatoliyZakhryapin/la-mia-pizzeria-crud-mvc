using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class PizzeriaController : Controller
    {
        public IActionResult Index()
        {
            List<Pizza> pizze = new List<Pizza>();

            pizze.Add(new Pizza("Margherita", "Pola di pomodoro, Fiordilatte, Origano, Basilico", "~/img/pizza_margherita.png", 7.99m));
            pizze.Add(new Pizza("Prosciutto e mozzarella", "Pola di pomodoro, Fiordilatte, Prosciutto", "~/img/pizza_prosciutto.png", 9.99m));
            pizze.Add(new Pizza("Divola", "Pola di pomodoro, Fiordilatte, Salame", "~/img/pizza_diavola.png", 10.99m));
            pizze.Add(new Pizza("Quattro formaggi", "Mozzarella, Gorgonzola, Fontina, Parmigiano, Polpa di pomodoro", "~/img/pizza_quattro_formaggi.png", 11.99m));
            pizze.Add(new Pizza("Napoletana", "Mozzarella, pomodoro, acciughe, origano, olio d’oliva Polpa di pomodoro", "~/img/pizza_napoletana.png", 10.99m));

            return View(pizze);
        }
    }
}
