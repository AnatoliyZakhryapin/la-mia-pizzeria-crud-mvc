﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            if (CountAllIngredients() == 0)
            {
                try
                {
                    PizzaManager.AddNewIngredient(new Ingredient("Polpa di pomodoro", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Acciughe", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Basilico", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Fiordilatte", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Fontina", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Gorgonzola", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Mozzarella", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Mozzarella di Bufala", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Olio d'oliva", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Origano", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Parmigiano", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Polpa di pomodoro", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Pomodoro", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Prosciuto", new List<Pizza>()));
                    PizzaManager.AddNewIngredient(new Ingredient("Salame", new List<Pizza>()));
                }
                catch (Exception) { }
            }
        }
        public static int CountAllIngredients()
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Ingredients.Count();
        }
        public static int CountAllPizzas()
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.Count();
        }
        public static void AddNewIngredient(Ingredient ingredient)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            db.Add(ingredient);
            db.SaveChanges();
        }

        public static void AddNewPizza(Pizza pizza)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            db.Add(pizza);
            db.SaveChanges();
        }

        public static List<Pizza> GetAllPizzas(bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            
            if (includeReferences)
                return db.Pizzas.Include(p => p.Category).ToList();
            return db.Pizzas.ToList();
        }

        public static Pizza GetPizzaByName(string name)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            return db.Pizzas.FirstOrDefault(p => p.Name == name);
        }

        public static Pizza GetPizzaById(int id, bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();

            if (includeReferences)
                return db.Pizzas.Include(p => p.Category).FirstOrDefault(p => p.PizzaId == id);
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
                pizzaToUpdate.CategoryId = pizzaUpdated.CategoryId;

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeletePizza(long id)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            var pizzaToDelete = db.Pizzas.Find(id);

            if(pizzaToDelete == null)
                return false;

            db.Pizzas.Remove(pizzaToDelete);
            db.SaveChanges();
            return true;
        }

        public static List<Category> GetAllCategories(bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
       
            if (includeReferences)
                return db.Categories.Include(p => p.Pizzas).ToList();

            return db.Categories.ToList(); ;
        }

        public static List<Ingredient> GetAllIngredients(bool includeReferences = true)
        {
            using PizzeriaDatabaseContext db = new PizzeriaDatabaseContext();
            if(includeReferences)
                return db.Ingredients.Include(i => i.Pizzas).ToList();
            return db.Ingredients.ToList();
        }

        public static PizzeriaFormModel CreatePizzeriaFormModel(Pizza pizza = null)
        {
    
            PizzeriaFormModel model = new PizzeriaFormModel();
            if (pizza != null)
            {
                model.Pizza = pizza;
                model.Categories = GetAllCategories();

                return model;
            }

            model.Pizza = new Pizza();
            model.Categories = GetAllCategories();

            model.Ingredients = new List<SelectListItem>();
            model.SelectedIngredients = new List<string>();

            List<Ingredient> ingredientsFormDb = PizzaManager.GetAllIngredients();

            foreach(Ingredient ingredient in ingredientsFormDb)
            {
                bool idSelected = model.Pizza.Ingredients?.Any(i => i.IngredientId == ingredient.IngredientId) == true;
                model.Ingredients.Add(new SelectListItem()
                {
                    Text = ingredient.Name,
                    Value = ingredient.IngredientId.ToString(),
                    Selected = idSelected
                }); 
                if (idSelected )
                    model.SelectedIngredients.Add(ingredient.IngredientId.ToString());
            }

            return model;
        }
    }
}
