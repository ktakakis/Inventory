using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(maxLength: 38, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceDueDate = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    shipmentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Shipment_shipmentId",
                        column: x => x.shipmentId,
                        principalTable: "Shipment",
                        principalColumn: "shipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_shipmentId",
                table: "Invoice",
                column: "shipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
