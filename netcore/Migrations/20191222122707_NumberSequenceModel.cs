using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class NumberSequenceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberSequence",
                columns: table => new
                {
                    NumberSequenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastNumber = table.Column<int>(nullable: false),
                    Module = table.Column<string>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    NumberSequenceName = table.Column<string>(nullable: false),
                    Prefix = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSequence", x => x.NumberSequenceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberSequence");
        }
    }
}
