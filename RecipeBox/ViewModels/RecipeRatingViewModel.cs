using System.ComponentModel.DataAnnotations;

namespace RecipeBox.ViewModels
{
  public class RecipeRatingViewModel
  {
    [Range(1, 10)]
    public int RecipeRating { get; set; }
  }
}