﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PodkarpackiLekarz.Infrastructure.Persistence;

#nullable disable

namespace PodkarpackiLekarz.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230610085327_changed_foreign_key_in_appoinments_table")]
    partial class changed_foreign_key_in_appoinments_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PLA")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PodkarpackiLekarz.Core.Calendars.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("DoctorId", "Date")
                        .IsUnique();

                    b.ToTable("Days", "PLA");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Base.IdentityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("IdentityUsers", "PLA");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Doctors.DoctorType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Speciality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DoctorTypes", "PLA");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Admins.Administrator", b =>
                {
                    b.HasBaseType("PodkarpackiLekarz.Core.Users.Base.IdentityUser");

                    b.ToTable("Administrators", "PLA");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Doctors.Doctor", b =>
                {
                    b.HasBaseType("PodkarpackiLekarz.Core.Users.Base.IdentityUser");

                    b.Property<int>("CredibilityConfirmationStatus")
                        .HasColumnType("int");

                    b.ToTable("Doctors", "PLA");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Patients.Patient", b =>
                {
                    b.HasBaseType("PodkarpackiLekarz.Core.Users.Base.IdentityUser");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("Pesel")
                        .IsUnique()
                        .HasFilter("[Pesel] IS NOT NULL");

                    b.ToTable("Patients", "PLA");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Calendars.Day", b =>
                {
                    b.HasOne("PodkarpackiLekarz.Core.Users.Doctors.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsMany("PodkarpackiLekarz.Core.Calendars.Slot", "Slots", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("DayId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("DayId");

                            b1.ToTable("Slots", "PLA");

                            b1.WithOwner()
                                .HasForeignKey("DayId");

                            b1.OwnsMany("PodkarpackiLekarz.Core.Calendars.Appoinment", "Appoinments", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<DateTime?>("AcceptedAt")
                                        .HasColumnType("datetime2");

                                    b2.Property<DateTime?>("CancelledAt")
                                        .HasColumnType("datetime2");

                                    b2.Property<DateTime?>("ClosedAt")
                                        .HasColumnType("datetime2");

                                    b2.Property<DateTime>("CreatedAt")
                                        .HasColumnType("datetime2");

                                    b2.Property<Guid>("PatientId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("SlotId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Status")
                                        .HasColumnType("int");

                                    b2.HasKey("Id");

                                    b2.HasIndex("PatientId");

                                    b2.HasIndex("SlotId");

                                    b2.ToTable("Appoinments", "PLA");

                                    b2.HasOne("PodkarpackiLekarz.Core.Users.Patients.Patient", null)
                                        .WithMany()
                                        .HasForeignKey("PatientId")
                                        .OnDelete(DeleteBehavior.NoAction)
                                        .IsRequired();

                                    b2.WithOwner()
                                        .HasForeignKey("SlotId");
                                });

                            b1.OwnsOne("PodkarpackiLekarz.Core.Calendars.Timeframe", "Timeframe", b2 =>
                                {
                                    b2.Property<Guid>("SlotId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<TimeSpan>("EndTime")
                                        .HasColumnType("time");

                                    b2.Property<TimeSpan>("StartTime")
                                        .HasColumnType("time");

                                    b2.HasKey("SlotId");

                                    b2.ToTable("Slots", "PLA");

                                    b2.WithOwner()
                                        .HasForeignKey("SlotId");
                                });

                            b1.Navigation("Appoinments");

                            b1.Navigation("Timeframe");
                        });

                    b.Navigation("Slots");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Base.IdentityUser", b =>
                {
                    b.OwnsOne("PodkarpackiLekarz.Core.Users.Base.UserSession", "Session", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("RefreshToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("RefreshTokenExpiryDate")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("Revoked")
                                .HasColumnType("bit");

                            b1.HasKey("UserId");

                            b1.ToTable("UserSessions", "PLA");

                            b1.WithOwner("IdentityUser")
                                .HasForeignKey("UserId");

                            b1.Navigation("IdentityUser");
                        });

                    b.Navigation("Session");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Admins.Administrator", b =>
                {
                    b.HasOne("PodkarpackiLekarz.Core.Users.Base.IdentityUser", null)
                        .WithOne()
                        .HasForeignKey("PodkarpackiLekarz.Core.Users.Admins.Administrator", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Doctors.Doctor", b =>
                {
                    b.HasOne("PodkarpackiLekarz.Core.Users.Base.IdentityUser", null)
                        .WithOne()
                        .HasForeignKey("PodkarpackiLekarz.Core.Users.Doctors.Doctor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PodkarpackiLekarz.Core.Users.Doctors.DoctorProfile", "DoctorProfile", b1 =>
                        {
                            b1.Property<Guid>("identityUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid?>("DoctorTypeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("MedicalLicenseNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("identityUserId");

                            b1.HasIndex("DoctorTypeId");

                            b1.ToTable("DoctorProfiles", "PLA");

                            b1.HasOne("PodkarpackiLekarz.Core.Users.Doctors.DoctorType", "DoctorType")
                                .WithMany()
                                .HasForeignKey("DoctorTypeId");

                            b1.WithOwner()
                                .HasForeignKey("identityUserId");

                            b1.Navigation("DoctorType");
                        });

                    b.Navigation("DoctorProfile");
                });

            modelBuilder.Entity("PodkarpackiLekarz.Core.Users.Patients.Patient", b =>
                {
                    b.HasOne("PodkarpackiLekarz.Core.Users.Base.IdentityUser", null)
                        .WithOne()
                        .HasForeignKey("PodkarpackiLekarz.Core.Users.Patients.Patient", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
