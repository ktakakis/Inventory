using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updateprodLine1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductLine",
                columns: table => new
                {
                    ProductLineId = table.Column<string>(maxLength: 38, nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLine", x => x.ProductLineId);
                    table.ForeignKey(
                        name: "FK_ProductLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_ProductId",
                table: "ProductLine",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLine");
        }
    }
}
