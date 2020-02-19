using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Moneytransfer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isIssued",
                table: "MoneyTransferOrder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReceived",
                table: "MoneyTransferOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isIssued",
                table: "MoneyTransferOrder");

            migrationBuilder.DropColumn(
                name: "isReceived",
                table: "MoneyTransferOrder");
        }
    }
}
