using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    [Required(ErrorMessage = "The recipe name can't be empty.")]
    public string RecipeName { get; set; }
    [Required(ErrorMessage = "The recipe ingredients can't be empty.")]
    public string Ingredients { get; set; }
    [Required(ErrorMessage = "The recipe instructions can't be empty.")]
    public string RecipeInstructions  { get; set; }
    public List<ItemTag> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}