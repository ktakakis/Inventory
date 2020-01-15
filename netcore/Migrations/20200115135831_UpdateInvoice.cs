using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerFax",
                table: "Invoice",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerMobilePhone",
                table: "Invoice",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerOfficePhone",
                table: "Invoice",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerWorkEmail",
                table: "Invoice",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerFax",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerMobilePhone",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerOfficePhone",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerWorkEmail",
                table: "Invoice");
        }
    }
}
