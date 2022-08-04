using Microsoft.EntityFrameworkCore.Migrations;

namespace MedLedger.Migrations
{
    public partial class AddingTaktTimeandResourcesMeasuresaandResourceUnits_0021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActualResources",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActualTaktTime",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EfficientResources",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EfficientTaktTime",
                table: "ServiceSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryUnit",
                table: "Inventory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualResources",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "ActualTaktTime",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "EfficientResources",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "EfficientTaktTime",
                table: "ServiceSchedule");

            migrationBuilder.DropColumn(
                name: "InventoryUnit",
                table: "Inventory");
        }
    }
}
