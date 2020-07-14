using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.FineOnlineSystem.Data.Migrations
{
    public partial class AddedPaymentDatefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Paymentdate",
            //    table: "FinePayments");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinePaymentDate",
                table: "FinePayments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinePaymentDate",
                table: "FinePayments");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Paymentdate",
            //    table: "FinePayments",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
