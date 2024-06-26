using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecipeBox.Models
{
  public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet <RecipeTag> RecipeTags { get; set; }
    public DbSet <RecipeIngredient> RecipeIngredients { get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { } 
  }
}