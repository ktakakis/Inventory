using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class moneytransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyTransferOrder",
                columns: table => new
                {
                    MoneyTransferOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    CashRepositoryFromCashRepositoryId = table.Column<string>(nullable: true),
                    CashRepositoryIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    CashRepositoryIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    CashRepositoryToCashRepositoryId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    MoneyTransferOrderDate = table.Column<DateTime>(nullable: false),
                    MoneyTransferOrderNumber = table.Column<string>(maxLength: 20, nullable: false),
                    MoneyTransferOrderStatus = table.Column<int>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransferOrder", x => x.MoneyTransferOrderId);
                    table.ForeignKey(
                        name: "FK_MoneyTransferOrder_CashRepository_CashRepositoryFromCashRepositoryId",
                        column: x => x.CashRepositoryFromCashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoneyTransferOrder_CashRepository_CashRepositoryToCashRepositoryId",
                        column: x => x.CashRepositoryToCashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransferOrder_CashRepositoryFromCashRepositoryId",
                table: "MoneyTransferOrder",
                column: "CashRepositoryFromCashRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransferOrder_CashRepositoryToCashRepositoryId",
                table: "MoneyTransferOrder",
                column: "CashRepositoryToCashRepositoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransferOrder");
        }
    }
}
