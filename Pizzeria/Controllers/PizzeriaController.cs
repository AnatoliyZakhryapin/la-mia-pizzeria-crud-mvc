using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class PizzeriaController : Controller
    {
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
            PizzeriaFormModel model = PizzaManager.CreatePizzeriaFormModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzeriaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories(false);
                return View("Create", data);
            }

            PizzaManager.AddNewPizza(data.Pizza);
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
                {
                    PizzeriaFormModel model = PizzaManager.CreatePizzeriaFormModel(pizzaToEdit);
                    return View(model);
                }
                else
                    return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(long id, PizzeriaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories(false);
                return View("Update", data);
            }

            bool result = PizzaManager.UpdatePizza(id, data.Pizza);

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            bool result = PizzaManager.DeletePizza(id);

            if (result == true) return RedirectToAction("Index");

            return NotFound();
        }
    }
}
