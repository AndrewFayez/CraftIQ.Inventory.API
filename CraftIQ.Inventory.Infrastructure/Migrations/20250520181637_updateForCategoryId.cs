using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftIQ.Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateForCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "_CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_CategoryId",
                table: "Categories",
                newName: "CategoryId");
        }
    }
}
