using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class SalesOrderLineUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductVAT",
                table: "SalesOrderLine",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductVATAmount",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SpecialTaxAmount",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "ProductVATAmount",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "SpecialTaxAmount",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "SalesOrderLine");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductVAT",
                table: "SalesOrderLine",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
