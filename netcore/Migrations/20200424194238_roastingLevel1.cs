using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class roastingLevel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoastingLevelId",
                table: "RoastingLog",
                maxLength: 38,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RoastingLog_RoastingLevelId",
                table: "RoastingLog",
                column: "RoastingLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoastingLog_RoastingLevel_RoastingLevelId",
                table: "RoastingLog",
                column: "RoastingLevelId",
                principalTable: "RoastingLevel",
                principalColumn: "RoastingLevelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoastingLog_RoastingLevel_RoastingLevelId",
                table: "RoastingLog");

            migrationBuilder.DropIndex(
                name: "IX_RoastingLog_RoastingLevelId",
                table: "RoastingLog");

            migrationBuilder.DropColumn(
                name: "RoastingLevelId",
                table: "RoastingLog");
        }
    }
}
