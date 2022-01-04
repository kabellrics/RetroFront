using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroFront.DataAccess.Migrations
{
    public partial class Epic_Origin_ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpicID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpicID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "OriginID",
                table: "Games");
        }
    }
}
