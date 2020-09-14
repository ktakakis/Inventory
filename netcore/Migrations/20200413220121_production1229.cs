using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class production1229 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLine_Shipment_shipmentId",
                table: "ProductionLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductionLine_shipmentId",
                table: "ProductionLine");

            migrationBuilder.DropColumn(
                name: "shipmentId",
                table: "ProductionLine");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_ProductionId",
                table: "ProductionLine",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLine_Production_ProductionId",
                table: "ProductionLine",
                column: "ProductionId",
                principalTable: "Production",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLine_Production_ProductionId",
                table: "ProductionLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductionLine_ProductionId",
                table: "ProductionLine");

            migrationBuilder.AddColumn<string>(
                name: "shipmentId",
                table: "ProductionLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_shipmentId",
                table: "ProductionLine",
                column: "shipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLine_Shipment_shipmentId",
                table: "ProductionLine",
                column: "shipmentId",
                principalTable: "Shipment",
                principalColumn: "shipmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
