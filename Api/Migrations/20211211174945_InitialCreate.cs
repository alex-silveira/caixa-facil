using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cashier",
                columns: table => new
                {
                    cashierId = table.Column<int>(type: "[int]", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    barCode = table.Column<int>(type: "[int]", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "[int]", nullable: false),
                    unitValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashier", x => x.cashierId);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    inventoryId = table.Column<int>(type: "[int]", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    barCode = table.Column<int>(type: "[int]", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "[int]", nullable: false),
                    unitValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.inventoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cashier");

            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
