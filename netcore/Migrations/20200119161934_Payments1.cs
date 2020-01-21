using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Payments1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_InvoiceId",
                table: "PaymentReceive",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceive_Invoice_InvoiceId",
                table: "PaymentReceive",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceive_Invoice_InvoiceId",
                table: "PaymentReceive");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceive_InvoiceId",
                table: "PaymentReceive");
        }
    }
}
