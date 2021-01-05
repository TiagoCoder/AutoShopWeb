using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Web.Migrations
{
    public partial class RemoveIdFieldAsPrimaryKeyFromServices_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Services_Orders",
                table: "Services_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Services_Orders_ServiceId",
                table: "Services_Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services_Orders",
                table: "Services_Orders",
                columns: new[] { "ServiceId", "OrderId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Services_Orders",
                table: "Services_Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services_Orders",
                table: "Services_Orders",
                columns: new[] { "Id", "ServiceId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_Orders_ServiceId",
                table: "Services_Orders",
                column: "ServiceId");
        }
    }
}
