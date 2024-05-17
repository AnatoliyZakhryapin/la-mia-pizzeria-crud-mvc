using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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

        public IActionResult PreShow(int id, string pizzaName)
        {
            TempData["PizzaId"] = id;

            return RedirectToAction("Show", new { name = pizzaName });
        }

        [Route("/Pizzeria/DettaglioPizza/{name}")]
        public IActionResult Show(string name)
        {
            if (TempData.ContainsKey("PizzaId"))
            {
                int id = (int)TempData["PizzaId"];

                Pizza pizzaFinded = PizzaManager.GetPizzaById(id);

                if (pizzaFinded != null)
                    return View(pizzaFinded);
                else
                    return View("errore");
            }
            else
            {
                return View("errore");
            }
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
