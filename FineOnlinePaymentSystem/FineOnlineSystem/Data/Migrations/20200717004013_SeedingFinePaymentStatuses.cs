using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.FineOnlineSystem.Data.Migrations
{
    public partial class SeedingFinePaymentStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FinePaymentStatuses",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "FinePaymentStatuses",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Paid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FinePaymentStatuses",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FinePaymentStatuses",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
