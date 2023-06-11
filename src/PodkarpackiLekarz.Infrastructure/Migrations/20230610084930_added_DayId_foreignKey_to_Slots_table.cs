using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodkarpackiLekarz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_DayId_foreignKey_to_Slots_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Days_SlotId",
                schema: "PLA",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_SlotId",
                schema: "PLA",
                table: "Slots");

            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                schema: "PLA",
                table: "Slots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Slots_DayId",
                schema: "PLA",
                table: "Slots",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Days_DayId",
                schema: "PLA",
                table: "Slots",
                column: "DayId",
                principalSchema: "PLA",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Days_DayId",
                schema: "PLA",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_DayId",
                schema: "PLA",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "DayId",
                schema: "PLA",
                table: "Slots");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_SlotId",
                schema: "PLA",
                table: "Slots",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Days_SlotId",
                schema: "PLA",
                table: "Slots",
                column: "SlotId",
                principalSchema: "PLA",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
