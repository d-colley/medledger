using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class AddClinicIDToServiceSchedule_0017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicID",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicID",
                table: "ServiceSchedule");
        }
    }
}
