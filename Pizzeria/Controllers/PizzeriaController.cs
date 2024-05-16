using Microsoft.AspNetCore.Mvc;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class PizzeriaController : Controller
    {
        public PizzeriaDatabaseContext DatabaseContext = new PizzeriaDatabaseContext();
        public IActionResult Index()
        {

            List<Pizza> listaPizzas = PizzaManager.GetAllPizzas();

            return View(listaPizzas);
        }
        [Route("/Pizzeria/DettaglioPizza/{name}")]
        public IActionResult Show(string name)
        {

            Pizza pizzaFinded = PizzaManager.GetPizzaByName(name);

            return View(pizzaFinded);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            PizzaManager.AddNewPizza(data);
            return RedirectToAction("Index");
        }
    }
}
