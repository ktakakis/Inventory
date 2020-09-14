using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class prodmaterial44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "branchId",
                table: "ProductionOrder",
                maxLength: 38,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_branchId",
                table: "ProductionOrder",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionOrder_Branch_branchId",
                table: "ProductionOrder",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "branchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionOrder_Branch_branchId",
                table: "ProductionOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductionOrder_branchId",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "ProductionOrder");
        }
    }
}
