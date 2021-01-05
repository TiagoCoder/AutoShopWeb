using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class AllowCascadeOnClient_Vehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientVehicles_Clients_ClientId",
                table: "ClientVehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientVehicles_Clients_ClientId",
                table: "ClientVehicles",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientVehicles_Clients_ClientId",
                table: "ClientVehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientVehicles_Clients_ClientId",
                table: "ClientVehicles",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
