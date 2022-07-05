using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class FixingDBNamingIssues_004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Professionals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professional",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "PatientID",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "PatientAddress",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "PatientDOB",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "PatientInsuranceProvider",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "PatientPhoneNumber",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Professional");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalID",
                table: "Professional",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClinicID",
                table: "Professional",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalName",
                table: "Professional",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Professional",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamID",
                table: "Professional",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professional",
                table: "Professional",
                column: "ProfessionalID");

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(nullable: false),
                    PatientDOB = table.Column<DateTime>(nullable: false),
                    PatientAddress = table.Column<string>(nullable: true),
                    PatientPhoneNumber = table.Column<int>(nullable: false),
                    PatientInsuranceProvider = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professional",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "ProfessionalID",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "ClinicID",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "ProfessionalName",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Professional");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "Professional");

            migrationBuilder.AddColumn<int>(
                name: "PatientID",
                table: "Professional",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "PatientAddress",
                table: "Professional",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientDOB",
                table: "Professional",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PatientInsuranceProvider",
                table: "Professional",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Professional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PatientPhoneNumber",
                table: "Professional",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Professional",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professional",
                table: "Professional",
                column: "PatientID");

            migrationBuilder.CreateTable(
                name: "Professionals",
                columns: table => new
                {
                    ProfessionalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicID = table.Column<int>(type: "int", nullable: false),
                    ProfessionalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professionals", x => x.ProfessionalID);
                });
        }
    }
}
