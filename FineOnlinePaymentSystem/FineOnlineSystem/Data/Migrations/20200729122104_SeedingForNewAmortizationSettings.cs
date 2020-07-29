using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.FineOnlineSystem.Data.Migrations
{
    public partial class SeedingForNewAmortizationSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AmortizationSettings",
                keyColumn: "AmortizationSettingsID",
                keyValue: 1,
                columns: new[] { "DaysToBeInJail", "PercentPerDay" },
                values: new object[] { 270, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AmortizationSettings",
                keyColumn: "AmortizationSettingsID",
                keyValue: 1,
                columns: new[] { "DaysToBeInJail", "PercentPerDay" },
                values: new object[] { 0, 6 });
        }
    }
}
