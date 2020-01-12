using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateInvoice6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCity",
                table: "Invoice",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCountry",
                table: "Invoice",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPostCode",
                table: "Invoice",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerStreet",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerTaxOffice",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerVATRegNumber",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Invoice",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficePhone",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VATNumber",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "branchName",
                table: "Invoice",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Invoice",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "Invoice",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Invoice",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street1",
                table: "Invoice",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCity",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerCountry",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerPostCode",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerStreet",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerTaxOffice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerVATRegNumber",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "OfficePhone",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "VATNumber",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "branchName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "customerName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "street1",
                table: "Invoice");
        }
    }
}
