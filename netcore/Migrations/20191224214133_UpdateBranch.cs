using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "street2",
                table: "Branch",
                newName: "email");

            migrationBuilder.AlterColumn<string>(
                name: "shipmentNumber",
                table: "Shipment",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Branch",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficePhone",
                table: "Branch",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Branch",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Branch",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VATNumber",
                table: "Branch",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "OfficePhone",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "VATNumber",
                table: "Branch");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Branch",
                newName: "street2");

            migrationBuilder.AlterColumn<string>(
                name: "shipmentNumber",
                table: "Shipment",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
