using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBox.Migrations
{
    public partial class AddRecipeRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeRating",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeRating",
                table: "Recipes");
        }
    }
}
