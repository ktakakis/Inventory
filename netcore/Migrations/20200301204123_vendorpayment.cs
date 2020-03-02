using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class vendorpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorPayment",
                columns: table => new
                {
                    VendorPaymentId = table.Column<string>(maxLength: 38, nullable: false),
                    CashRepositoryId = table.Column<string>(maxLength: 38, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 38, nullable: true),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentNumber = table.Column<string>(nullable: true),
                    PaymentTypeId = table.Column<string>(maxLength: 38, nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    purchaseOrderId = table.Column<string>(maxLength: 38, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPayment", x => x.VendorPaymentId);
                    table.ForeignKey(
                        name: "FK_VendorPayment_CashRepository_CashRepositoryId",
                        column: x => x.CashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPayment_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPayment_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPayment_PurchaseOrder_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayment_CashRepositoryId",
                table: "VendorPayment",
                column: "CashRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayment_EmployeeId",
                table: "VendorPayment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayment_PaymentTypeId",
                table: "VendorPayment",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayment_purchaseOrderId",
                table: "VendorPayment",
                column: "purchaseOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorPayment");
        }
    }
}
