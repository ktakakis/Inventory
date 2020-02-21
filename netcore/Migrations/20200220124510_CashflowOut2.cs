using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class CashflowOut2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashflowOut_Warehouse_CashRepositoryTowarehouseId",
                table: "CashflowOut");

            migrationBuilder.DropForeignKey(
                name: "FK_CashflowOut_TransferOrder_MoneyTransferOrderId",
                table: "CashflowOut");

            migrationBuilder.RenameColumn(
                name: "CashRepositoryTowarehouseId",
                table: "CashflowOut",
                newName: "CashRepositoryToCashRepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CashflowOut_CashRepositoryTowarehouseId",
                table: "CashflowOut",
                newName: "IX_CashflowOut_CashRepositoryToCashRepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowOut_CashRepository_CashRepositoryToCashRepositoryId",
                table: "CashflowOut",
                column: "CashRepositoryToCashRepositoryId",
                principalTable: "CashRepository",
                principalColumn: "CashRepositoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowOut_MoneyTransferOrder_MoneyTransferOrderId",
                table: "CashflowOut",
                column: "MoneyTransferOrderId",
                principalTable: "MoneyTransferOrder",
                principalColumn: "MoneyTransferOrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashflowOut_CashRepository_CashRepositoryToCashRepositoryId",
                table: "CashflowOut");

            migrationBuilder.DropForeignKey(
                name: "FK_CashflowOut_MoneyTransferOrder_MoneyTransferOrderId",
                table: "CashflowOut");

            migrationBuilder.RenameColumn(
                name: "CashRepositoryToCashRepositoryId",
                table: "CashflowOut",
                newName: "CashRepositoryTowarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_CashflowOut_CashRepositoryToCashRepositoryId",
                table: "CashflowOut",
                newName: "IX_CashflowOut_CashRepositoryTowarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowOut_Warehouse_CashRepositoryTowarehouseId",
                table: "CashflowOut",
                column: "CashRepositoryTowarehouseId",
                principalTable: "Warehouse",
                principalColumn: "warehouseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashflowOut_TransferOrder_MoneyTransferOrderId",
                table: "CashflowOut",
                column: "MoneyTransferOrderId",
                principalTable: "TransferOrder",
                principalColumn: "transferOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
