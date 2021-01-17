using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class GameIMG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emulators_Systemes_SystemeID",
                table: "Emulators");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Emulators_EmulatorID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_EmulatorID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Emulators_SystemeID",
                table: "Emulators");

            migrationBuilder.RenameColumn(
                name: "Box3dart",
                table: "Games",
                newName: "Screenshoot");

            migrationBuilder.RenameColumn(
                name: "Banner",
                table: "Games",
                newName: "Genre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Screenshoot",
                table: "Games",
                newName: "Box3dart");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Games",
                newName: "Banner");

            migrationBuilder.CreateIndex(
                name: "IX_Games_EmulatorID",
                table: "Games",
                column: "EmulatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Emulators_SystemeID",
                table: "Emulators",
                column: "SystemeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Emulators_Systemes_SystemeID",
                table: "Emulators",
                column: "SystemeID",
                principalTable: "Systemes",
                principalColumn: "SystemeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Emulators_EmulatorID",
                table: "Games",
                column: "EmulatorID",
                principalTable: "Emulators",
                principalColumn: "EmulatorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
