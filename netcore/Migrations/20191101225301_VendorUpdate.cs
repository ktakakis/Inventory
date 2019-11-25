using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class VendorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxNumber",
                table: "Vendor",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Vendor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fax",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobilePhone",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "officePhone",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personalEmail",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workEmail",
                table: "Vendor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxNumber",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "fax",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "mobilePhone",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "officePhone",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "personalEmail",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "workEmail",
                table: "Vendor");
        }
    }
}
