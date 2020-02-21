using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class CashflowIn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashflowIn",
                columns: table => new
                {
                    CashflowInId = table.Column<string>(maxLength: 38, nullable: false),
                    CashRepositoryFromCashRepositoryId = table.Column<string>(nullable: true),
                    CashRepositoryIdFrom = table.Column<string>(maxLength: 38, nullable: true),
                    CashRepositoryIdTo = table.Column<string>(maxLength: 38, nullable: true),
                    CashRepositoryToCashRepositoryId = table.Column<string>(nullable: true),
                    CashflowInDate = table.Column<DateTime>(nullable: false),
                    CashflowInNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    MoneyTransferOrderId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashflowIn", x => x.CashflowInId);
                    table.ForeignKey(
                        name: "FK_CashflowIn_CashRepository_CashRepositoryFromCashRepositoryId",
                        column: x => x.CashRepositoryFromCashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashflowIn_CashRepository_CashRepositoryToCashRepositoryId",
                        column: x => x.CashRepositoryToCashRepositoryId,
                        principalTable: "CashRepository",
                        principalColumn: "CashRepositoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashflowIn_MoneyTransferOrder_MoneyTransferOrderId",
                        column: x => x.MoneyTransferOrderId,
                        principalTable: "MoneyTransferOrder",
                        principalColumn: "MoneyTransferOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashflowIn_CashRepositoryFromCashRepositoryId",
                table: "CashflowIn",
                column: "CashRepositoryFromCashRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CashflowIn_CashRepositoryToCashRepositoryId",
                table: "CashflowIn",
                column: "CashRepositoryToCashRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CashflowIn_MoneyTransferOrderId",
                table: "CashflowIn",
                column: "MoneyTransferOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashflowIn");
        }
    }
}
