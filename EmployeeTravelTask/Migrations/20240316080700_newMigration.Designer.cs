﻿// <auto-generated />
using System;
using EmployeeTravelTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeTravelTask.Migrations
{
    [DbContext(typeof(TravelPlannerContext))]
    [Migration("20240316080700_newMigration")]
    partial class newMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeTravelTask.Models.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.GradesHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("AssignedOn")
                        .HasColumnType("date");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("GradeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GradeId");

                    b.ToTable("GradesHistory", (string)null);
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Goa"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Shimla"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Manali"
                        });
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.TravelBudgetAllocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApprovedBudget")
                        .HasColumnType("int");

                    b.Property<string>("ApprovedHotelStarRating")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("ApprovedModeOfTravel")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("TravelRequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TravelRequestId");

                    b.ToTable("TravelBudgetAllocations");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.TravelRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Priority")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("PurposeOfTravel")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("RaisedByEmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestApprovedOn")
                        .HasColumnType("date");

                    b.Property<DateTime?>("RequestRaisedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("RequestStatus")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<int?>("ToBeApprovedByHrid")
                        .HasColumnType("int")
                        .HasColumnName("ToBeApprovedByHRId");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("date");

                    b.HasKey("RequestId")
                        .HasName("PK__TravelRe__33A8517A59D76985");

                    b.HasIndex("LocationId");

                    b.ToTable("TravelRequests", t =>
                        {
                            t.HasCheckConstraint("CK__TravelRequest__1234", "ToDate > FromDate");
                        });
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.User", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentGradeId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("LastName")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Role")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("EmployeeId")
                        .HasName("PK__Users__7AD04F11C5DAD8BC");

                    b.HasIndex("CurrentGradeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.GradesHistory", b =>
                {
                    b.HasOne("EmployeeTravelTask.Models.User", "Employee")
                        .WithMany("GradesHistories")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__GradesHis__Emplo__35BCFE0A");

                    b.HasOne("EmployeeTravelTask.Models.Grade", "Grades")
                        .WithMany("GradesHistories")
                        .HasForeignKey("GradeId")
                        .HasConstraintName("FK__GradesHis__Grade__34C8D9D1");

                    b.Navigation("Employee");

                    b.Navigation("Grades");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.TravelBudgetAllocation", b =>
                {
                    b.HasOne("EmployeeTravelTask.Models.TravelRequest", "TravelRequest")
                        .WithMany("TravelBudgetAllocations")
                        .HasForeignKey("TravelRequestId")
                        .HasConstraintName("FK__TravelBud__Trave__2B3F6F97");

                    b.Navigation("TravelRequest");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.TravelRequest", b =>
                {
                    b.HasOne("EmployeeTravelTask.Models.Location", "Location")
                        .WithMany("TravelRequests")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK__TravelReq__Locat__276EDEB3");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.User", b =>
                {
                    b.HasOne("EmployeeTravelTask.Models.Grade", "CurrentGrade")
                        .WithMany("Users")
                        .HasForeignKey("CurrentGradeId")
                        .HasConstraintName("User_fk");

                    b.Navigation("CurrentGrade");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.Grade", b =>
                {
                    b.Navigation("GradesHistories");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.Location", b =>
                {
                    b.Navigation("TravelRequests");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.TravelRequest", b =>
                {
                    b.Navigation("TravelBudgetAllocations");
                });

            modelBuilder.Entity("EmployeeTravelTask.Models.User", b =>
                {
                    b.Navigation("GradesHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
