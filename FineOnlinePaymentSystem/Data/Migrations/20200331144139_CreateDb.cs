using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FineOnlinePaymentSystem.Data.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmortizationSettings",
                columns: table => new
                {
                    AmortizationSettingsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PercentPerDay = table.Column<int>(nullable: false),
                    DaysBeforeAmortization = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmortizationSettings", x => x.AmortizationSettingsID);
                });

            migrationBuilder.CreateTable(
                name: "CaseStatuses",
                columns: table => new
                {
                    CaseStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStatuses", x => x.CaseStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Offences",
                columns: table => new
                {
                    OffenseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offences", x => x.OffenseID);
                });

            migrationBuilder.CreateTable(
                name: "OffenderStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffenderStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    OfficerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(25)", nullable: false),
                    FroceNumber = table.Column<int>(nullable: false),
                    Contact = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.OfficerID);
                    table.ForeignKey(
                        name: "FK_Officers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    RelativeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(25)", nullable: false),
                    PIN = table.Column<string>(type: "varchar(18)", nullable: false),
                    Contact = table.Column<long>(nullable: false),
                    IdentityUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.RelativeID);
                    table.ForeignKey(
                        name: "FK_Relatives_AspNetUsers_IdentityUserID",
                        column: x => x.IdentityUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offenders",
                columns: table => new
                {
                    OffenderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(25)", nullable: false),
                    PIN = table.Column<string>(type: "varchar(18)", nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Nationality = table.Column<string>(type: "varchar(25)", nullable: false),
                    HomeAddress = table.Column<string>(type: "varchar(100)", nullable: false),
                    StatusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offenders", x => x.OffenderID);
                    table.ForeignKey(
                        name: "FK_Offenders_OffenderStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "OffenderStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNumber = table.Column<int>(nullable: false),
                    CaseDescription = table.Column<string>(type: "varchar(max)", nullable: false),
                    CrimeLocation = table.Column<string>(type: "varchar(100)", nullable: false),
                    DateOfCrime = table.Column<DateTime>(nullable: false),
                    DateOfArrest = table.Column<DateTime>(nullable: true),
                    CourtDate = table.Column<DateTime>(nullable: true),
                    OfficerID = table.Column<int>(nullable: false),
                    OffenceID = table.Column<int>(nullable: false),
                    CaseStatusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_Cases_CaseStatuses_CaseStatusID",
                        column: x => x.CaseStatusID,
                        principalTable: "CaseStatuses",
                        principalColumn: "CaseStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Offences_OffenceID",
                        column: x => x.OffenceID,
                        principalTable: "Offences",
                        principalColumn: "OffenseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Officers_OfficerID",
                        column: x => x.OfficerID,
                        principalTable: "Officers",
                        principalColumn: "OfficerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseOffenders",
                columns: table => new
                {
                    CaseID = table.Column<int>(nullable: false),
                    OffenderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseOffenders", x => new { x.CaseID, x.OffenderID });
                    table.ForeignKey(
                        name: "FK_CaseOffenders_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseOffenders_Offenders_OffenderID",
                        column: x => x.OffenderID,
                        principalTable: "Offenders",
                        principalColumn: "OffenderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    CaseID = table.Column<int>(nullable: false),
                    OffenderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineID);
                    table.ForeignKey(
                        name: "FK_Fines_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fines_Offenders_OffenderID",
                        column: x => x.OffenderID,
                        principalTable: "Offenders",
                        principalColumn: "OffenderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amortizations",
                columns: table => new
                {
                    AmortizationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaysOverstayed = table.Column<int>(nullable: false),
                    Percent = table.Column<int>(nullable: false),
                    AmortizationAmount = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    CaseID = table.Column<int>(nullable: false),
                    FineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amortizations", x => x.AmortizationID);
                    table.ForeignKey(
                        name: "FK_Amortizations_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Amortizations_Fines_FineID",
                        column: x => x.FineID,
                        principalTable: "Fines",
                        principalColumn: "FineID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FinePayments",
                columns: table => new
                {
                    FinePaymentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Paymentdate = table.Column<DateTime>(nullable: false),
                    AmortizationAmount = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    AmountPayable = table.Column<decimal>(type: "Decimal(10,2)", nullable: false),
                    Attachment = table.Column<byte>(nullable: false),
                    AmortizationID = table.Column<int>(nullable: false),
                    RelativeID = table.Column<int>(nullable: false),
                    FineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinePayments", x => x.FinePaymentID);
                    table.ForeignKey(
                        name: "FK_FinePayments_Amortizations_AmortizationID",
                        column: x => x.AmortizationID,
                        principalTable: "Amortizations",
                        principalColumn: "AmortizationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinePayments_Fines_FineID",
                        column: x => x.FineID,
                        principalTable: "Fines",
                        principalColumn: "FineID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FinePayments_Relatives_RelativeID",
                        column: x => x.RelativeID,
                        principalTable: "Relatives",
                        principalColumn: "RelativeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AmortizationSettings",
                columns: new[] { "AmortizationSettingsID", "DaysBeforeAmortization", "PercentPerDay" },
                values: new object[] { 1, 3, 6 });

            migrationBuilder.InsertData(
                table: "CaseStatuses",
                columns: new[] { "CaseStatusID", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "Offences",
                columns: new[] { "OffenseID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Robbery" },
                    { 2, null, "Assault" },
                    { 3, null, "Harassment" },
                    { 4, null, "Vandalism" },
                    { 5, null, "Theft" },
                    { 6, null, "Public Indecency(Indecent Exposure)" },
                    { 7, null, "Public Indecency(Open Container)" },
                    { 8, null, "Bribery" },
                    { 9, null, "Stalking" }
                });

            migrationBuilder.InsertData(
                table: "OffenderStatuses",
                columns: new[] { "StatusID", "Name" },
                values: new object[,]
                {
                    { 1, "In Custody" },
                    { 2, "Released" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amortizations_CaseID",
                table: "Amortizations",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Amortizations_FineID",
                table: "Amortizations",
                column: "FineID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseOffenders_OffenderID",
                table: "CaseOffenders",
                column: "OffenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseNumber",
                table: "Cases",
                column: "CaseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseStatusID",
                table: "Cases",
                column: "CaseStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_OffenceID",
                table: "Cases",
                column: "OffenceID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_OfficerID",
                table: "Cases",
                column: "OfficerID");

            migrationBuilder.CreateIndex(
                name: "IX_FinePayments_AmortizationID",
                table: "FinePayments",
                column: "AmortizationID");

            migrationBuilder.CreateIndex(
                name: "IX_FinePayments_FineID",
                table: "FinePayments",
                column: "FineID");

            migrationBuilder.CreateIndex(
                name: "IX_FinePayments_RelativeID",
                table: "FinePayments",
                column: "RelativeID");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CaseID",
                table: "Fines",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_OffenderID",
                table: "Fines",
                column: "OffenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Offenders_PIN",
                table: "Offenders",
                column: "PIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offenders_StatusID",
                table: "Offenders",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Officers_FroceNumber",
                table: "Officers",
                column: "FroceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Officers_UserID",
                table: "Officers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Relatives_IdentityUserID",
                table: "Relatives",
                column: "IdentityUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Relatives_PIN",
                table: "Relatives",
                column: "PIN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmortizationSettings");

            migrationBuilder.DropTable(
                name: "CaseOffenders");

            migrationBuilder.DropTable(
                name: "FinePayments");

            migrationBuilder.DropTable(
                name: "Amortizations");

            migrationBuilder.DropTable(
                name: "Relatives");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Offenders");

            migrationBuilder.DropTable(
                name: "CaseStatuses");

            migrationBuilder.DropTable(
                name: "Offences");

            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "OffenderStatuses");
        }
    }
}
