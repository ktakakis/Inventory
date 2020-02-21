using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class CashflowOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashflowOut",
                columns: table => new
                {
                    CashflowOutId = table.Column<string>(maxLength: 38, nullable: false),
                    CashRepositoryFromCashRepositoryId = table.Column<string>(nullable: true),
                    CashRepositoryIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    CashRepositoryIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    CashRepositoryTowarehouseId = table.Column<string>(nullable: true),
                    CashflowOutDate = table.Column<DateTime>(nullable: false),
                    CashflowOutNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    MoneyTransferOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashflowOut", x => x.CashflowOutId);
                    table.ForeignKey(
                        name: "FK_CashflowOut_CashRepository_CashRepositoryFromCashRepositoryId",
                        column: x => x.CashRepositoryFromCashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashflowOut_Warehouse_CashRepositoryTowarehouseId",
                        column: x => x.CashRepositoryTowarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "warehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashflowOut_TransferOrder_MoneyTransferOrderId",
                        column: x => x.MoneyTransferOrderId,
                        principalTable: "TransferOrder",
                        principalColumn: "transferOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashflowOut_CashRepositoryFromCashRepositoryId",
                table: "CashflowOut",
                column: "CashRepositoryFromCashRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CashflowOut_CashRepositoryTowarehouseId",
                table: "CashflowOut",
                column: "CashRepositoryTowarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_CashflowOut_MoneyTransferOrderId",
                table: "CashflowOut",
                column: "MoneyTransferOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashflowOut");
        }
    }
}
