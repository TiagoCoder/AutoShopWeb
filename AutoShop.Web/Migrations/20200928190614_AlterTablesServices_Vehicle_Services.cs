using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class AlterTablesServices_Vehicle_Services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle_Services",
                table: "Vehicle_Services");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Vehicle_Services");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "ClientVehicleId",
                table: "Vehicle_Services",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle_Services",
                table: "Vehicle_Services",
                columns: new[] { "ServiceId", "ClientVehicleId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle_Services",
                table: "Vehicle_Services");

            migrationBuilder.AlterColumn<int>(
                name: "ClientVehicleId",
                table: "Vehicle_Services",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Vehicle_Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle_Services",
                table: "Vehicle_Services",
                columns: new[] { "ServiceId", "VehicleId" });
        }
    }
}
