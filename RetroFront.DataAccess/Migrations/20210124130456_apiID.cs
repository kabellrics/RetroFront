using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class apiID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IGDBID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RAWGID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SGDBID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "Emulators",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IGDBID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RAWGID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SGDBID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "Emulators");
        }
    }
}
