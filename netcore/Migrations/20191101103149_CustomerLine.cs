using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class CustomerLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fax",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "jobTitle",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "middleName",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "mobilePhone",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "nickName",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "officePhone",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "personalEmail",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "salutation",
                table: "CustomerLine");

            migrationBuilder.RenameColumn(
                name: "workEmail",
                table: "CustomerLine",
                newName: "street1");

            migrationBuilder.AddColumn<string>(
                name: "LocLat",
                table: "CustomerLine",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocLong",
                table: "CustomerLine",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "CustomerLine",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "CustomerLine",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "CustomerLine",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customerAddressLineId",
                table: "CustomerLine",
                maxLength: 38,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "province",
                table: "CustomerLine",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocLat",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "LocLong",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "city",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "country",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "customerAddressLineId",
                table: "CustomerLine");

            migrationBuilder.DropColumn(
                name: "province",
                table: "CustomerLine");

            migrationBuilder.RenameColumn(
                name: "street1",
                table: "CustomerLine",
                newName: "workEmail");

            migrationBuilder.AddColumn<string>(
                name: "fax",
                table: "CustomerLine",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "CustomerLine",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "CustomerLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "jobTitle",
                table: "CustomerLine",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "CustomerLine",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "middleName",
                table: "CustomerLine",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobilePhone",
                table: "CustomerLine",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nickName",
                table: "CustomerLine",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "officePhone",
                table: "CustomerLine",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personalEmail",
                table: "CustomerLine",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "salutation",
                table: "CustomerLine",
                nullable: false,
                defaultValue: 0);
        }
    }
}
