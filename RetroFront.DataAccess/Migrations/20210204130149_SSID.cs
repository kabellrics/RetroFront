using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class SSID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenScraperID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenScraperID",
                table: "Games");
        }
    }
}
