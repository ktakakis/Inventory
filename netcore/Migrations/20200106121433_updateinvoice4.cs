using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updateinvoice4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "InvoiceLine",
                maxLength: 38,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 38,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceId",
                table: "InvoiceLine",
                maxLength: 38,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 38);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
