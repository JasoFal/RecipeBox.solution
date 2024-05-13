using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    [Required(ErrorMessage = "The recipe name can't be empty.")]
    public string RecipeName { get; set; }
    [Required(ErrorMessage = "The recipe instructions can't be empty.")]
    public string RecipeInstructions  { get; set; }
    public int RecipeRating { get; set; } = 0;
    public List<Ingredient> Ingredients { get; set; }
    public List<RecipeTag> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}