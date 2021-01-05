using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class removeVehicle_Services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle_Services");

            migrationBuilder.AddColumn<int>(
                name: "Qtd",
                table: "Services_Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientVehicleId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientVehicleId",
                table: "Orders",
                column: "ClientVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ClientVehicles_ClientVehicleId",
                table: "Orders",
                column: "ClientVehicleId",
                principalTable: "ClientVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ClientVehicles_ClientVehicleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClientVehicleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Qtd",
                table: "Services_Orders");

            migrationBuilder.DropColumn(
                name: "ClientVehicleId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "Vehicle_Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false),
                    ClientVehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle_Services", x => new { x.ServiceId, x.ClientVehicleId });
                    table.ForeignKey(
                        name: "FK_Vehicle_Services_ClientVehicles_ClientVehicleId",
                        column: x => x.ClientVehicleId,
                        principalTable: "ClientVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_Services_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Services_ClientVehicleId",
                table: "Vehicle_Services",
                column: "ClientVehicleId");
        }
    }
}
