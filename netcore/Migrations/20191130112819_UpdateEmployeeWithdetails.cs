using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateEmployeeWithdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Employee",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Employee",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fax",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobilePhone",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "officePhone",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personalEmail",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "province",
                table: "Employee",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street1",
                table: "Employee",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "street2",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "workEmail",
                table: "Employee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "fax",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "mobilePhone",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "officePhone",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "personalEmail",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "province",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "street1",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "street2",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "workEmail",
                table: "Employee");
        }
    }
}
