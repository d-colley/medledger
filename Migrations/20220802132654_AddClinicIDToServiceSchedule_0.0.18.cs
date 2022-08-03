using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class AddClinicIDToServiceSchedule_0018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentAppointments",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxAppointments",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceID",
                table: "Appointment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAppointments",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "MaxAppointments",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "Appointment");
        }
    }
}
