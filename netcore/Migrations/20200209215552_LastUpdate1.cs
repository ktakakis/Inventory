using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class LastUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CashRepositoryId",
                table: "PaymentReceive",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_CashRepositoryId",
                table: "PaymentReceive",
                column: "CashRepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceive_CashRepository_CashRepositoryId",
                table: "PaymentReceive",
                column: "CashRepositoryId",
                principalTable: "CashRepository",
                principalColumn: "CashRepositoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceive_CashRepository_CashRepositoryId",
                table: "PaymentReceive");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceive_CashRepositoryId",
                table: "PaymentReceive");

            migrationBuilder.DropColumn(
                name: "CashRepositoryId",
                table: "PaymentReceive");
        }
    }
}
