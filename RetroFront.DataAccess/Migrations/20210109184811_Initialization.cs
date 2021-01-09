using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Systemes",
                columns: table => new
                {
                    SystemeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Shortname = table.Column<string>(type: "TEXT", nullable: true),
                    Platform = table.Column<string>(type: "TEXT", nullable: true),
                    Theme = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systemes", x => x.SystemeID);
                });

            migrationBuilder.CreateTable(
                name: "Emulators",
                columns: table => new
                {
                    EmulatorID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Comand = table.Column<string>(type: "TEXT", nullable: true),
                    Extension = table.Column<string>(type: "TEXT", nullable: true),
                    SystemeID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emulators", x => x.EmulatorID);
                    table.ForeignKey(
                        name: "FK_Emulators_Systemes_SystemeID",
                        column: x => x.SystemeID,
                        principalTable: "Systemes",
                        principalColumn: "SystemeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    Desc = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<string>(type: "TEXT", nullable: true),
                    Editeur = table.Column<string>(type: "TEXT", nullable: true),
                    Dev = table.Column<string>(type: "TEXT", nullable: true),
                    Boxart = table.Column<string>(type: "TEXT", nullable: true),
                    Fanart = table.Column<string>(type: "TEXT", nullable: true),
                    EmulatorID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Games_Emulators_EmulatorID",
                        column: x => x.EmulatorID,
                        principalTable: "Emulators",
                        principalColumn: "EmulatorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emulators_SystemeID",
                table: "Emulators",
                column: "SystemeID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_EmulatorID",
                table: "Games",
                column: "EmulatorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Emulators");

            migrationBuilder.DropTable(
                name: "Systemes");
        }
    }
}
