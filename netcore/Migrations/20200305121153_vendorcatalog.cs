using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class vendorcatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorCatalog",
                columns: table => new
                {
                    VendorCatalogId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    VendorCatalogName = table.Column<string>(maxLength: 50, nullable: false),
                    VendorId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCatalog", x => x.VendorCatalogId);
                    table.ForeignKey(
                        name: "FK_VendorCatalog_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorCatalogLine",
                columns: table => new
                {
                    VendorCatalogLineId = table.Column<string>(maxLength: 38, nullable: false),
                    Discount = table.Column<decimal>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    VendorCatalogId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCatalogLine", x => x.VendorCatalogLineId);
                    table.ForeignKey(
                        name: "FK_VendorCatalogLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorCatalogLine_VendorCatalog_VendorCatalogId",
                        column: x => x.VendorCatalogId,
                        principalTable: "VendorCatalog",
                        principalColumn: "VendorCatalogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorCatalog_VendorId",
                table: "VendorCatalog",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCatalogLine_ProductId",
                table: "VendorCatalogLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCatalogLine_VendorCatalogId",
                table: "VendorCatalogLine",
                column: "VendorCatalogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorCatalogLine");

            migrationBuilder.DropTable(
                name: "VendorCatalog");
        }
    }
}
