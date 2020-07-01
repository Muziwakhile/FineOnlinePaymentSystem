using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.FineOnlineSystem.Data.Migrations
{
    public partial class AddedFineStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FineStatusID",
                table: "Fines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FineStatuses",
                columns: table => new
                {
                    FineStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FineStatuses", x => x.FineStatusID);
                });

            migrationBuilder.InsertData(
                table: "FineStatuses",
                columns: new[] { "FineStatusID", "Name" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "FineStatuses",
                columns: new[] { "FineStatusID", "Name" },
                values: new object[] { 2, "Paid" });

            migrationBuilder.CreateIndex(
                name: "IX_Fines_FineStatusID",
                table: "Fines",
                column: "FineStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_FineStatuses_FineStatusID",
                table: "Fines",
                column: "FineStatusID",
                principalTable: "FineStatuses",
                principalColumn: "FineStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_FineStatuses_FineStatusID",
                table: "Fines");

            migrationBuilder.DropTable(
                name: "FineStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Fines_FineStatusID",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "FineStatusID",
                table: "Fines");
        }
    }
}
