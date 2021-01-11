using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emulators_Systemes_SystemeID",
                table: "Emulators");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Emulators_EmulatorID",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "EmulatorID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SystemeID",
                table: "Emulators",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emulators_Systemes_SystemeID",
                table: "Emulators");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Emulators_EmulatorID",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "EmulatorID",
                table: "Games",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SystemeID",
                table: "Emulators",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Emulators_Systemes_SystemeID",
                table: "Emulators",
                column: "SystemeID",
                principalTable: "Systemes",
                principalColumn: "SystemeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Emulators_EmulatorID",
                table: "Games",
                column: "EmulatorID",
                principalTable: "Emulators",
                principalColumn: "EmulatorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
