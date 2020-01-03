using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updatesalesorder5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "referenceNumberExternal",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "referenceNumberInternal",
                table: "SalesOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "referenceNumberExternal",
                table: "SalesOrder",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referenceNumberInternal",
                table: "SalesOrder",
                maxLength: 30,
                nullable: true);
        }
    }
}
