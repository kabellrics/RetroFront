using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class Update_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Systemes");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Systemes");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Games",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Systemes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Systemes",
                type: "TEXT",
                nullable: true);
        }
    }
}
