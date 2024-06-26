using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using RecipeBox.ViewModels;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Recipe> userRecipes = _db.Recipes
        .Where(e => e.User.Id == currentUser.Id)
        .OrderByDescending(recipe => recipe.RecipeRating)
        .ToList();
      return View(userRecipes);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe)
    {
      if (!ModelState.IsValid)
      {
        return View(recipe);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        recipe.User = currentUser;
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Recipe thisRecipe = _db.Recipes
        .Include(rec => rec.IJoinEntities)
        .ThenInclude(join => join.Ingredient)
        .Include(r => r.JoinEntities)
        .ThenInclude(j => j.Tag)
        .FirstOrDefault(r => r.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult AddTag(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Category");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddTag(Recipe recipe, int tagId)
    {
      #nullable enable
        RecipeTag? joinEntity = _db.RecipeTags.FirstOrDefault(j => (j.TagId == tagId && j.RecipeId == recipe.RecipeId)); 
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() { TagId = tagId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }

    public ActionResult Edit(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      if (!ModelState.IsValid)
      {
        return View(recipe);
      }
      else
      {
        _db.Recipes.Update(recipe);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      RecipeTag joinEntry = _db.RecipeTags.FirstOrDefault(e => e.RecipeTagId == joinId);
      _db.RecipeTags.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult RateRecipe(int id)
    {
      ViewBag.RecipeId = _db.Recipes.FirstOrDefault(r => r.RecipeId == id).RecipeId;
      return View();
    }

    [HttpPost]
    public ActionResult RateRecipe(RecipeRatingViewModel model, int id)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.RecipeId = _db.Recipes.FirstOrDefault(r => r.RecipeId == id).RecipeId;
        return View();
      }
      else
      {
        Recipe thisRecipe = _db.Recipes.FirstOrDefault(r => r.RecipeId == id);
        thisRecipe.RecipeRating = model.RecipeRating;
        _db.Recipes.Update(thisRecipe);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult AddIngredient(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(rec => rec.RecipeId == id);
      ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddIngredient(Recipe recipe, int ingredientId)
    {
      #nullable enable
      RecipeIngredient? joinEntity = _db.RecipeIngredients.FirstOrDefault(join => (join.IngredientId == ingredientId && join.RecipeId == recipe.RecipeId));
      #nullable disable
      if (joinEntity == null && ingredientId != 0)
      {
        _db.RecipeIngredients.Add(new RecipeIngredient() { IngredientId = ingredientId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }
  }
}