using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contoso.FoodDelivery.BusinessLogic.Migrations;

public partial class initialcreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Restaurants",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Address_AddressLine1 = table.Column<string>(type: "TEXT", nullable: false),
                Address_AddressLine2 = table.Column<string>(type: "TEXT", nullable: true),
                Address_ZipCode = table.Column<string>(type: "TEXT", nullable: false),
                Address_City = table.Column<string>(type: "TEXT", nullable: false),
                Address_State = table.Column<string>(type: "TEXT", nullable: false),
                Plan = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Restaurants", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RestaurantMenuItem",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: false),
                Price = table.Column<decimal>(type: "TEXT", nullable: false),
                Featured = table.Column<bool>(type: "INTEGER", nullable: false),
                RestaurantId = table.Column<Guid>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RestaurantMenuItem", x => x.Id);
                table.ForeignKey(
                    name: "FK_RestaurantMenuItem_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_RestaurantMenuItem_RestaurantId",
            table: "RestaurantMenuItem",
            column: "RestaurantId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RestaurantMenuItem");

        migrationBuilder.DropTable(
            name: "Restaurants");
    }
}

