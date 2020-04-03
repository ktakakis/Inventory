using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class employeepayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayment",
                columns: table => new
                {
                    EmployeePaymentId = table.Column<string>(maxLength: 38, nullable: false),
                    CashRepositoryId = table.Column<string>(maxLength: 38, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentNumber = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayment", x => x.EmployeePaymentId);
                    table.ForeignKey(
                        name: "FK_EmployeePayment_CashRepository_CashRepositoryId",
                        column: x => x.CashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayment_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayment_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayment_CashRepositoryId",
                table: "EmployeePayment",
                column: "CashRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayment_EmployeeId",
                table: "EmployeePayment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayment_PaymentTypeId",
                table: "EmployeePayment",
                column: "PaymentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayment");
        }
    }
}
