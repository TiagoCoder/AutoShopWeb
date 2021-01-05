using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class AddPropertiesToClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Registration",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Registration",
                table: "Vehicles");
        }
    }
}
