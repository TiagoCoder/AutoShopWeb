using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class Add_Entity_VehiclesClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Clients_ClientId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Registration",
                table: "Vehicles",
                newName: "Year");

            migrationBuilder.CreateTable(
                name: "ClientVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Registration = table.Column<string>(nullable: true),
                    VehicleBrandModelId = table.Column<int>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientVehicles_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientVehicles_Vehicles_VehicleBrandModelId",
                        column: x => x.VehicleBrandModelId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles_Clients",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles_Clients", x => new { x.ClientId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_Vehicles_Clients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Clients_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientVehicles_ClientId",
                table: "ClientVehicles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientVehicles_VehicleBrandModelId",
                table: "ClientVehicles",
                column: "VehicleBrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Clients_VehicleId",
                table: "Vehicles_Clients",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientVehicles");

            migrationBuilder.DropTable(
                name: "Vehicles_Clients");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Vehicles",
                newName: "Registration");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicles",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Clients_ClientId",
                table: "Vehicles",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
