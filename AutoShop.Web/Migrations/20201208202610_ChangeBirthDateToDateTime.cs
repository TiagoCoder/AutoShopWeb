using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class ChangeBirthDateToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleInfoId",
                table: "ClientVehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ClientVehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleInfoId",
                table: "ClientVehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ClientVehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
