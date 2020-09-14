using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class prolineupdate12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionOrderLine_Production_ProductionId",
                table: "ProductionOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductionOrderLine_ProductionId",
                table: "ProductionOrderLine");

            migrationBuilder.DropColumn(
                name: "ProductionId",
                table: "ProductionOrderLine");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ProductionId",
                table: "ProductionOrderLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderLine_ProductionId",
                table: "ProductionOrderLine",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionOrderLine_Production_ProductionId",
                table: "ProductionOrderLine",
                column: "ProductionId",
                principalTable: "Production",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
