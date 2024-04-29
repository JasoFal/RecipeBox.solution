using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectName.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectName.Controllers
{
  public class ProjectController : Controller
  {
    private readonly ClassNameContext _db;

    public ProjectController(ClassNameContext db)
    {
      _db = db;
    }
  }
}