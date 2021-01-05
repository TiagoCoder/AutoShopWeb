using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class Remove_Unused_Tables_RemoveTableVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientVehicles_Vehicles_VehicleBrandModelId",
                table: "ClientVehicles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Vehicles_Clients");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "VehicleBrandModelId",
                table: "ClientVehicles",
                newName: "VehicleInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientVehicles_VehicleBrandModelId",
                table: "ClientVehicles",
                newName: "IX_ClientVehicles_VehicleInfoId");

            migrationBuilder.CreateTable(
                name: "VehicleInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInfo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientVehicles_VehicleInfo_VehicleInfoId",
                table: "ClientVehicles",
                column: "VehicleInfoId",
                principalTable: "VehicleInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientVehicles_VehicleInfo_VehicleInfoId",
                table: "ClientVehicles");

            migrationBuilder.DropTable(
                name: "VehicleInfo");

            migrationBuilder.RenameColumn(
                name: "VehicleInfoId",
                table: "ClientVehicles",
                newName: "VehicleBrandModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientVehicles_VehicleInfoId",
                table: "ClientVehicles",
                newName: "IX_ClientVehicles_VehicleBrandModelId");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    LastPurchase = table.Column<DateTime>(nullable: false),
                    LastSale = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles_Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
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
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Clients_VehicleId",
                table: "Vehicles_Clients",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientVehicles_Vehicles_VehicleBrandModelId",
                table: "ClientVehicles",
                column: "VehicleBrandModelId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
