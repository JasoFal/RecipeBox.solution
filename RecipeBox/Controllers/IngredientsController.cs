using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBox.Controllers
{
  public class IngredientsController : Controller
  {
    private readonly RecipeBoxContext _db;

    public IngredientsController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Ingredients.ToList());
    }

    public ActionResult Details(int id)
    {
      Ingredient thisIngredient = _db.Ingredients
        .Include(ing => ing.IJoinEntities)
        .ThenInclude(join => join.Recipe)
        .FirstOrDefault(ing => ing.IngredientId == id);
      return View(thisIngredient);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Ingredient ingredient)
    {
      if (!ModelState.IsValid)
      {
        return View(ingredient);
      }
      else
      {
        _db.Ingredients.Add(ingredient);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult AddRecipe(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ing => ing.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      return View(thisIngredient);
    }

    [HttpPost]
    public ActionResult AddRecipe(Ingredient ingredient, int recipeId)
    {
      #nullable enable
      RecipeIngredient? joinEntity = _db.RecipeIngredients.FirstOrDefault(join => (join.RecipeId == recipeId && join.IngredientId == ingredient.IngredientId));
      #nullable disable
      if (joinEntity == null && recipeId != 0)
      {
        _db.RecipeIngredients.Add(new RecipeIngredient() { RecipeId = recipeId, IngredientId = ingredient.IngredientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = ingredient.IngredientId });
    }

    public ActionResult Edit(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ing => ing.IngredientId == id);
      return View(thisIngredient);
    }

    [HttpPost]
    public ActionResult Edit(Ingredient ingredient)
    {
      if (!ModelState.IsValid)
      {
        return View(ingredient);
      }
      else
      {
        _db.Ingredients.Update(ingredient);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
  }
}