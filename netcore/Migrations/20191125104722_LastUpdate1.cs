using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class LastUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLine_City_CityId",
                table: "CustomerLine");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLine_CityId",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CustomerLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "CustomerLine",
                maxLength: 38,
                nullable: true);

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
    }
}
