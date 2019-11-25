using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class CustomerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "province",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "street1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "street2",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Customer",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Customer",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "province",
                table: "Customer",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street1",
                table: "Customer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street2",
                table: "Customer",
                maxLength: 50,
                nullable: true);
        }
    }
}
