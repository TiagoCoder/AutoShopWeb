using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class Change_VehicleServices_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Services_Vehicles_VehicleId",
                table: "Vehicle_Services");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_Services_VehicleId",
                table: "Vehicle_Services");

            migrationBuilder.AddColumn<int>(
                name: "ClientVehicleId",
                table: "Vehicle_Services",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Services_ClientVehicleId",
                table: "Vehicle_Services",
                column: "ClientVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Services_ClientVehicles_ClientVehicleId",
                table: "Vehicle_Services",
                column: "ClientVehicleId",
                principalTable: "ClientVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Services_ClientVehicles_ClientVehicleId",
                table: "Vehicle_Services");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_Services_ClientVehicleId",
                table: "Vehicle_Services");

            migrationBuilder.DropColumn(
                name: "ClientVehicleId",
                table: "Vehicle_Services");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Services_VehicleId",
                table: "Vehicle_Services",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Services_Vehicles_VehicleId",
                table: "Vehicle_Services",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
