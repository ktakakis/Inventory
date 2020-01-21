using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Payments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<string>(maxLength: 38, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PaymentTypeName = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceive",
                columns: table => new
                {
                    PaymentReceiveId = table.Column<string>(maxLength: 38, nullable: false),
                    InvoiceId = table.Column<string>(maxLength: 38, nullable: true),
                    IsFullPayment = table.Column<bool>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentReceiveName = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceive", x => x.PaymentReceiveId);
                    table.ForeignKey(
                        name: "FK_PaymentReceive_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_PaymentTypeId",
                table: "PaymentReceive",
                column: "PaymentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentReceive");

            migrationBuilder.DropTable(
                name: "PaymentType");
        }
    }
}
