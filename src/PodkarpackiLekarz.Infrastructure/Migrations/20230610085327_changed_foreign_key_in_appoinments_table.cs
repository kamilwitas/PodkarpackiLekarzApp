using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodkarpackiLekarz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed_foreign_key_in_appoinments_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotId",
                schema: "PLA",
                table: "Slots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SlotId",
                schema: "PLA",
                table: "Slots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
