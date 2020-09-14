using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class prodmaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductionId",
                table: "ProductionOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMaterial",
                table: "Product",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsMaterial",
                table: "Product");
        }
    }
}
