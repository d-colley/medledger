using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class TaktTimeEngine_0019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTimeAvailable",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxTimeAvailable",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTimeAvailable",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "MaxTimeAvailable",
                table: "ServiceSchedule");
        }
    }
}
