using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateSalesOrderLineFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSpecialTaxAmount",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalWithSpecialTax",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSpecialTaxAmount",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "TotalWithSpecialTax",
                table: "SalesOrderLine");
        }
    }
}
