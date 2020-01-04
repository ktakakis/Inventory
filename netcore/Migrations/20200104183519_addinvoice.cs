using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class addinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalBeforeDiscount",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalProductVAT",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalWithSpecialTax",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "totalDiscountAmount",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "totalOrderAmount",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    InvoiceLineId = table.Column<string>(maxLength: 38, nullable: false),
                    Discount = table.Column<decimal>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    InvoiceId = table.Column<string>(maxLength: 38, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    ProductVAT = table.Column<decimal>(nullable: false),
                    ProductVATAmount = table.Column<decimal>(nullable: false),
                    Qty = table.Column<float>(nullable: false),
                    SpecialTaxAmount = table.Column<decimal>(nullable: false),
                    SpecialTaxDiscount = table.Column<decimal>(nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalBeforeDiscount = table.Column<decimal>(nullable: true),
                    TotalSpecialTaxAmount = table.Column<decimal>(nullable: false),
                    TotalWithSpecialTax = table.Column<decimal>(nullable: false),
                    UnitCost = table.Column<decimal>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ProductId",
                table: "InvoiceLine",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "TotalBeforeDiscount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalProductVAT",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalWithSpecialTax",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "totalDiscountAmount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "totalOrderAmount",
                table: "Invoice");
        }
    }
}
