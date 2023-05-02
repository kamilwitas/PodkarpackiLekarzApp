using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodkarpackiLekarz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PLA");

            migrationBuilder.CreateTable(
                name: "DoctorTypes",
                schema: "PLA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Speciality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                schema: "PLA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "PLA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CredibilityConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_IdentityUsers_Id",
                        column: x => x.Id,
                        principalSchema: "PLA",
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfiles",
                schema: "PLA",
                columns: table => new
                {
                    identityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfiles", x => x.identityUserId);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_DoctorTypes_DoctorTypeId",
                        column: x => x.DoctorTypeId,
                        principalSchema: "PLA",
                        principalTable: "DoctorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_Doctors_identityUserId",
                        column: x => x.identityUserId,
                        principalSchema: "PLA",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_DoctorTypeId",
                schema: "PLA",
                table: "DoctorProfiles",
                column: "DoctorTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorProfiles",
                schema: "PLA");

            migrationBuilder.DropTable(
                name: "DoctorTypes",
                schema: "PLA");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "PLA");

            migrationBuilder.DropTable(
                name: "IdentityUsers",
                schema: "PLA");
        }
    }
}
