using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class lastupdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Shipment",
                maxLength: 38,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_EmployeeId",
                table: "Shipment",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipment_Employee_EmployeeId",
                table: "Shipment",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipment_Employee_EmployeeId",
                table: "Shipment");

            migrationBuilder.DropIndex(
                name: "IX_Shipment_EmployeeId",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Shipment");
        }
    }
}
