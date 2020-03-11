using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updateemployees1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCommission",
                table: "Employee",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSales",
                table: "Employee",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCommission",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "Employee");

            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
