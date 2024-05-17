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

        public IActionResult PreUpdate(int id, string pizzaName)
        {
            TempData["PizzaId"] = id;

            return RedirectToAction("Update", new { name = pizzaName });
        }

        [HttpGet]
        public IActionResult Update(string name)
        {
            if (TempData.ContainsKey("PizzaId"))
            {
                int id = (int)TempData["PizzaId"];

                Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);

                if (pizzaToEdit != null)
                    return View(pizzaToEdit);
                else
                    return View("errore");
            }
            else
            {
                return View("errore");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(long id, Pizza data)
        {
            if(!ModelState.IsValid)
            {
                return View("Update", data);
            }

            bool result = PizzaManager.UpdatePizza(id, data);

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();
        }

    }
}
