using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace practnet.Migrations
{
    public partial class addDbHasBeenSeededtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NeedsSeedingSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HasBeenSeeded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeedsSeedingSet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NeedsSeedingSet");
        }
    }
}
