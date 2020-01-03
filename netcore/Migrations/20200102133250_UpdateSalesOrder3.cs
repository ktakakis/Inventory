using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateSalesOrder3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "SalesOrder",
                maxLength: 38,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_EmployeeId",
                table: "SalesOrder",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Employee_EmployeeId",
                table: "SalesOrder",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Employee_EmployeeId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_EmployeeId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SalesOrder");
        }
    }
}
