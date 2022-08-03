using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class AddClinicServices_0013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicServices",
                table: "Clinic",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicServices",
                table: "Clinic");
        }
    }
}
