using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class roastingLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoastingLevel",
                columns: table => new
                {
                    RoastingLevelId = table.Column<string>(maxLength: 38, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    RoastingLevelName = table.Column<string>(maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoastingLevel", x => x.RoastingLevelId);
                });

            migrationBuilder.CreateTable(
                name: "RoastingLog",
                columns: table => new
                {
                    RoastingLogId = table.Column<string>(maxLength: 38, nullable: false),
                    AfterFillingTemp = table.Column<decimal>(nullable: false),
                    AmbientTemp = table.Column<decimal>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    FinishWeight = table.Column<int>(nullable: false),
                    FirstCrackTime = table.Column<DateTime>(nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    LossPercent = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 38, nullable: true),
                    RoasterName = table.Column<string>(maxLength: 50, nullable: false),
                    RoastingDate = table.Column<DateTime>(nullable: false),
                    SecondCrackTime = table.Column<DateTime>(nullable: false),
                    StartTemp = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    StartWeight = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoastingLog", x => x.RoastingLogId);
                    table.ForeignKey(
                        name: "FK_RoastingLog_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoastingLog_ProductId",
                table: "RoastingLog",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoastingLevel");

            migrationBuilder.DropTable(
                name: "RoastingLog");
        }
    }
}
