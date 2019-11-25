using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyActivity",
                table: "Customer",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "OrderDiscount",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SalesCount",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxDiscount",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Customer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VATRegNumber",
                table: "Customer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Customer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fax",
                table: "Customer",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobilePhone",
                table: "Customer",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "officePhone",
                table: "Customer",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workEmail",
                table: "Customer",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CompanyActivity",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "OrderDiscount",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SalesCount",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TaxDiscount",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "VATRegNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "fax",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "mobilePhone",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "officePhone",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "workEmail",
                table: "Customer");
        }
    }
}
