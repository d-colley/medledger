using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class AddingProfessionalExperience_0010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Professional");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalExpYears",
                table: "Professional",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalSpecialty",
                table: "Professional",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessionalExpYears",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "ProfessionalSpecialty",
                table: "Professional");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Professional",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
