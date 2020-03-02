using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Category2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryCategoryID",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParenetCategoryID",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryCategoryID",
                table: "Category",
                newName: "ParentCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryCategoryID",
                table: "Category",
                newName: "IX_Category_ParentCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryID",
                table: "Category",
                column: "ParentCategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryID",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryID",
                table: "Category",
                newName: "ParentCategoryCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryID",
                table: "Category",
                newName: "IX_Category_ParentCategoryCategoryID");

            migrationBuilder.AddColumn<int>(
                name: "ParenetCategoryID",
                table: "Category",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryCategoryID",
                table: "Category",
                column: "ParentCategoryCategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
