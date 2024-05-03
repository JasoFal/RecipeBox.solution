using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Tag
  {
    public int TagId { get; set; }
    [Required(ErrorMessage = "Tag category can't be left empty.")]
    public string Category { get; set; }
    public List<RecipeTag> JoinEntities { get; }
  }
}