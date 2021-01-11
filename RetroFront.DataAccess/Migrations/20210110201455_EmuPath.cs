using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class EmuPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comand",
                table: "Emulators",
                newName: "Command");

            migrationBuilder.AddColumn<string>(
                name: "Chemin",
                table: "Emulators",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chemin",
                table: "Emulators");

            migrationBuilder.RenameColumn(
                name: "Command",
                table: "Emulators",
                newName: "Comand");
        }
    }
}
