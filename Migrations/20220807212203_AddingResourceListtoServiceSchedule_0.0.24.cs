using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class AddingResourceListtoServiceSchedule_0024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClinicOverprovision",
                table: "Clinic",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ClinicUnderprovision",
                table: "Clinic",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicOverprovision",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "ClinicUnderprovision",
                table: "Clinic");
        }
    }
}
