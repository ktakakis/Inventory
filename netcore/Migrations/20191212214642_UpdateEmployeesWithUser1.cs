using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class UpdateEmployeesWithUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_userId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Employee",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_userId",
                table: "Employee",
                newName: "IX_Employee_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_UserId",
                table: "Employee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_UserId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employee",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                newName: "IX_Employee_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_userId",
                table: "Employee",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
