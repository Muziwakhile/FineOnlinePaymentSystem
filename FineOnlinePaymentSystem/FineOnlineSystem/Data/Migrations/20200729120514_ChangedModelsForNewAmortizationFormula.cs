using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.FineOnlineSystem.Data.Migrations
{
    public partial class ChangedModelsForNewAmortizationFormula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Fines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaysToBeInJail",
                table: "AmortizationSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AmortizationAmountPerDay",
                table: "Amortizations",
                type: "Decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "DaysToBeInJail",
                table: "AmortizationSettings");

            migrationBuilder.DropColumn(
                name: "AmortizationAmountPerDay",
                table: "Amortizations");
        }
    }
}
