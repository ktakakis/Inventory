using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class prodmaterial449 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Production_Branch_branchId",
                table: "Production");

            migrationBuilder.DropIndex(
                name: "IX_Production_branchId",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "Production");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "branchId",
                table: "Production",
                maxLength: 38,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Production_branchId",
                table: "Production",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Production_Branch_branchId",
                table: "Production",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "branchId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
