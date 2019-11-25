using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class SalesOrderAddDeliveryAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryAddress",
                table: "SalesOrder");

            migrationBuilder.AddColumn<string>(
                name: "customerLineId",
                table: "SalesOrder",
                maxLength: 38,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_customerLineId",
                table: "SalesOrder",
                column: "customerLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_CustomerLine_customerLineId",
                table: "SalesOrder",
                column: "customerLineId",
                principalTable: "CustomerLine",
                principalColumn: "customerLineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_CustomerLine_customerLineId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_customerLineId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "customerLineId",
                table: "SalesOrder");

            migrationBuilder.AddColumn<string>(
                name: "deliveryAddress",
                table: "SalesOrder",
                maxLength: 50,
                nullable: true);
        }
    }
}
