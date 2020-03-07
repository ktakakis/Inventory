using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class purcaseorderupdate45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryAddress",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "referenceNumberInternal",
                table: "PurchaseOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "deliveryAddress",
                table: "PurchaseOrder",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "referenceNumberInternal",
                table: "PurchaseOrder",
                maxLength: 30,
                nullable: true);
        }
    }
}
