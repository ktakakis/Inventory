using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updateprodLine111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProductLine");

            migrationBuilder.AddColumn<string>(
                name: "ComponentId",
                table: "ProductLine",
                maxLength: 38,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_ComponentId",
                table: "ProductLine",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Product_ComponentId",
                table: "ProductLine",
                column: "ComponentId",
                principalTable: "Product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Product_ComponentId",
                table: "ProductLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductLine_ComponentId",
                table: "ProductLine");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "ProductLine");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ProductLine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
