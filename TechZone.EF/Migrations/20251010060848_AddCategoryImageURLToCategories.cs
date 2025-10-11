using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechZone.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryImageURLToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categoryimageurl",
                table: "Categories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categoryimageurl",
                table: "Categories");
        }
    }
}
