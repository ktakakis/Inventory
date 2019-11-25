using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class TaxAmaountToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SpecialTaxValue",
                table: "Product",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxDiscount",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SpecialTaxValue",
                table: "Product",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "TaxDiscount",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
