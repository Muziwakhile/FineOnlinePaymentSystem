using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.FineOnlineSystem.Data.Migrations
{
    public partial class AddedFinePaymentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinePaymentStatusID",
                table: "FinePayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FinePaymentStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinePaymentStatuses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinePayments_FinePaymentStatusID",
                table: "FinePayments",
                column: "FinePaymentStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinePayments_FinePaymentStatuses_FinePaymentStatusID",
                table: "FinePayments",
                column: "FinePaymentStatusID",
                principalTable: "FinePaymentStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinePayments_FinePaymentStatuses_FinePaymentStatusID",
                table: "FinePayments");

            migrationBuilder.DropTable(
                name: "FinePaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_FinePayments_FinePaymentStatusID",
                table: "FinePayments");

            migrationBuilder.DropColumn(
                name: "FinePaymentStatusID",
                table: "FinePayments");
        }
    }
}
