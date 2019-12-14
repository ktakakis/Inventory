using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateEmployeesWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "street2",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "workEmail",
                table: "Employee",
                newName: "userId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_userId",
                table: "Employee",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_userId",
                table: "Employee",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_userId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_userId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Employee",
                newName: "workEmail");

            migrationBuilder.AlterColumn<string>(
                name: "workEmail",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street2",
                table: "Employee",
                maxLength: 50,
                nullable: true);
        }
    }
}
