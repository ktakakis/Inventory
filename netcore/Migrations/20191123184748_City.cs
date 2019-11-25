using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "CustomerLine",
                maxLength: 38,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<string>(maxLength: 38, nullable: false),
                    CityName = table.Column<string>(maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLine_CityId",
                table: "CustomerLine",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLine_City_CityId",
                table: "CustomerLine",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLine_City_CityId",
                table: "CustomerLine");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLine_CityId",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CustomerLine");
        }
    }
}
