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
  }
}