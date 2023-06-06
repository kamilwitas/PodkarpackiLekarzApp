using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodkarpackiLekarz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_calendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                schema: "PLA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "PLA",
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                schema: "PLA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timeframe_StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Timeframe_EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    SlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_Days_SlotId",
                        column: x => x.SlotId,
                        principalSchema: "PLA",
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appoinments",
                schema: "PLA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appoinments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "PLA",
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appoinments_Slots_SlotId",
                        column: x => x.SlotId,
                        principalSchema: "PLA",
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_PatientId",
                schema: "PLA",
                table: "Appoinments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_SlotId",
                schema: "PLA",
                table: "Appoinments",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_Date",
                schema: "PLA",
                table: "Days",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Days_DoctorId_Date",
                schema: "PLA",
                table: "Days",
                columns: new[] { "DoctorId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slots_SlotId",
                schema: "PLA",
                table: "Slots",
                column: "SlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appoinments",
                schema: "PLA");

            migrationBuilder.DropTable(
                name: "Slots",
                schema: "PLA");

            migrationBuilder.DropTable(
                name: "Days",
                schema: "PLA");
        }
    }
}
