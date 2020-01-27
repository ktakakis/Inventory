using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updatepaymentreceive2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "PaymentReceive",
                maxLength: 38,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Employee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentReceiver",
                table: "Employee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_EmployeeId",
                table: "PaymentReceive",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceive_Employee_EmployeeId",
                table: "PaymentReceive",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceive_Employee_EmployeeId",
                table: "PaymentReceive");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceive_EmployeeId",
                table: "PaymentReceive");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PaymentReceive");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PaymentReceiver",
                table: "Employee");
        }
    }
}
