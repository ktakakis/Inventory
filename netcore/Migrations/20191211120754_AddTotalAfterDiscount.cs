using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class AddTotalAfterDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAfterDiscount",
                table: "SalesOrderLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAfterDiscount",
                table: "SalesOrderLine");
        }
    }
}
