using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class NewDataForGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStart",
                table: "Games",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbTimeStarted",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastStart",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NbTimeStarted",
                table: "Games");
        }
    }
}
