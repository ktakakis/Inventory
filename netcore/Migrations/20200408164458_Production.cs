using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Production : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionOrder",
                columns: table => new
                {
                    ProductionOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    HasChild = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderDate = table.Column<DateTime>(nullable: false),
                    ProductionOrderStatus = table.Column<int>(nullable: false),
                    RequiredDeliveryDate = table.Column<DateTime>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    salesOrderNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder", x => x.ProductionOrderId);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrderLine",
                columns: table => new
                {
                    ProductionOrderLineId = table.Column<string>(maxLength: 38, nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    ProductionOrderId = table.Column<string>(maxLength: 38, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrderLine", x => x.ProductionOrderLineId);
                    table.ForeignKey(
                        name: "FK_ProductionOrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionOrderLine_ProductionOrder_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrder",
                        principalColumn: "ProductionOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderLine_ProductId",
                table: "ProductionOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderLine_ProductionOrderId",
                table: "ProductionOrderLine",
                column: "ProductionOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionOrderLine");

            migrationBuilder.DropTable(
                name: "ProductionOrder");
        }
    }
}
