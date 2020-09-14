using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class production122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    ProductionId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    ProductionDate = table.Column<DateTime>(nullable: false),
                    ProductionNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ProductionOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    warehouseId = table.Column<string>(maxLength: 38, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.ProductionId);
                    table.ForeignKey(
                        name: "FK_Production_ProductionOrder_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrder",
                        principalColumn: "ProductionOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Production_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Production_Warehouse_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLine",
                columns: table => new
                {
                    ProductionLineId = table.Column<string>(maxLength: 38, nullable: false),
                    ProductionId = table.Column<string>(maxLength: 38, nullable: true),
                    branchId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    productId = table.Column<string>(maxLength: 38, nullable: true),
                    qty = table.Column<float>(nullable: false),
                    shipmentId = table.Column<string>(nullable: true),
                    warehouseId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLine", x => x.ProductionLineId);
                    table.ForeignKey(
                        name: "FK_ProductionLine_Branch_branchId",
                        column: x => x.branchId,
                        principalTable: "Branch",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionLine_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionLine_Shipment_shipmentId",
                        column: x => x.shipmentId,
                        principalTable: "Shipment",
                        principalColumn: "shipmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionLine_Warehouse_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProductionOrderId",
                table: "Production",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_branchId",
                table: "Production",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_warehouseId",
                table: "Production",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_branchId",
                table: "ProductionLine",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_productId",
                table: "ProductionLine",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_shipmentId",
                table: "ProductionLine",
                column: "shipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_warehouseId",
                table: "ProductionLine",
                column: "warehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Production");

            migrationBuilder.DropTable(
                name: "ProductionLine");
        }
    }
}
