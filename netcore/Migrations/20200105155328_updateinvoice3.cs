using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updateinvoice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
