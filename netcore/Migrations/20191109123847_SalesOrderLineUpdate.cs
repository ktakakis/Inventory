using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class SalesOrderLineUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Product_productId",
                table: "SalesOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_salesOrderId",
                table: "SalesOrderLine");

            migrationBuilder.RenameColumn(
                name: "totalAmount",
                table: "SalesOrderLine",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "salesOrderId",
                table: "SalesOrderLine",
                newName: "SalesOrderId");

            migrationBuilder.RenameColumn(
                name: "qty",
                table: "SalesOrderLine",
                newName: "Qty");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "SalesOrderLine",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "SalesOrderLine",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "discountAmount",
                table: "SalesOrderLine",
                newName: "DiscountAmount");

            migrationBuilder.RenameColumn(
                name: "salesOrderLineId",
                table: "SalesOrderLine",
                newName: "SalesOrderLineId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrderLine_salesOrderId",
                table: "SalesOrderLine",
                newName: "IX_SalesOrderLine_SalesOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrderLine_productId",
                table: "SalesOrderLine",
                newName: "IX_SalesOrderLine_ProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductVAT",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Product_ProductId",
                table: "SalesOrderLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                table: "SalesOrderLine",
                column: "SalesOrderId",
                principalTable: "SalesOrder",
                principalColumn: "salesOrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Product_ProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "ProductVAT",
                table: "SalesOrderLine");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SalesOrderLine",
                newName: "totalAmount");

            migrationBuilder.RenameColumn(
                name: "SalesOrderId",
                table: "SalesOrderLine",
                newName: "salesOrderId");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "SalesOrderLine",
                newName: "qty");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SalesOrderLine",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SalesOrderLine",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "SalesOrderLine",
                newName: "discountAmount");

            migrationBuilder.RenameColumn(
                name: "SalesOrderLineId",
                table: "SalesOrderLine",
                newName: "salesOrderLineId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrderLine_SalesOrderId",
                table: "SalesOrderLine",
                newName: "IX_SalesOrderLine_salesOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine",
                newName: "IX_SalesOrderLine_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Product_productId",
                table: "SalesOrderLine",
                column: "productId",
                principalTable: "Product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_salesOrderId",
                table: "SalesOrderLine",
                column: "salesOrderId",
                principalTable: "SalesOrder",
                principalColumn: "salesOrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
