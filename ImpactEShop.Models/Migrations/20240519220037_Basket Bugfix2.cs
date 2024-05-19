using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImpactEShop.Models.Migrations
{
    /// <inheritdoc />
    public partial class BasketBugfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "BasketItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
