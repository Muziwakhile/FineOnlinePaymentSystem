using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.Data.Migrations
{
    public partial class ChangeFinePaymentFieldType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Attachment",
                table: "FinePayments",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Attachment",
                table: "FinePayments",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte[]));
        }
    }
}
