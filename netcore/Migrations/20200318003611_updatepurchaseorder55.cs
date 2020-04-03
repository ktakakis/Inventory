using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class updatepurchaseorder55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "filename",
                table: "PurchaseOrder");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "PurchaseOrder",
                newName: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "PurchaseOrder",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "filename",
                table: "PurchaseOrder",
                maxLength: 40,
                nullable: true);
        }
    }
}
