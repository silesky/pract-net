using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace practnet.Migrations
{
    public partial class initaligration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimerGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimerGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Paused = table.Column<bool>(nullable: false),
                    StartTime = table.Column<int>(nullable: false),
                    Ticking = table.Column<bool>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    TimerGroupId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timers_TimerGroups_TimerGroupId",
                        column: x => x.TimerGroupId,
                        principalTable: "TimerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timers_TimerGroupId",
                table: "Timers",
                column: "TimerGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timers");

            migrationBuilder.DropTable(
                name: "TimerGroups");
        }
    }
}
