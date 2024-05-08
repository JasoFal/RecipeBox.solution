using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBox.Controllers
{
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;

    public TagsController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Tags.ToList());
    }

    public ActionResult Details(int id)
    {
      Tag thisTag = _db.Tags
        .Include(t => t.JoinEntities)
        .ThenInclude(j => j.Recipe)
        .FirstOrDefault(t => t.TagId == id);
      return View(thisTag);
    }
  }
}