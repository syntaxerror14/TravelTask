using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeTravelTask.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    LastName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CurrentGradeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__7AD04F11C5DAD8BC", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "User_fk",
                        column: x => x.CurrentGradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TravelRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaisedByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ToBeApprovedByHRId = table.Column<int>(type: "int", nullable: true),
                    RequestRaisedOn = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    ToDate = table.Column<DateTime>(type: "date", nullable: false),
                    PurposeOfTravel = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    RequestStatus = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    RequestApprovedOn = table.Column<DateTime>(type: "date", nullable: true),
                    Priority = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TravelRe__33A8517A59D76985", x => x.RequestId);
                    table.CheckConstraint("CK__TravelRequest__1234", "ToDate > FromDate");
                    table.ForeignKey(
                        name: "FK__TravelReq__Locat__276EDEB3",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GradesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedOn = table.Column<DateTime>(type: "date", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    GradeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK__GradesHis__Emplo__35BCFE0A",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__GradesHis__Grade__34C8D9D1",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TravelBudgetAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelRequestId = table.Column<int>(type: "int", nullable: true),
                    ApprovedBudget = table.Column<int>(type: "int", nullable: true),
                    ApprovedModeOfTravel = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedHotelStarRating = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelBudgetAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK__TravelBud__Trave__2B3F6F97",
                        column: x => x.TravelRequestId,
                        principalTable: "TravelRequests",
                        principalColumn: "RequestId");
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Goa" },
                    { 2, "Shimla" },
                    { 3, "Manali" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradesHistory_EmployeeId",
                table: "GradesHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradesHistory_GradeId",
                table: "GradesHistory",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelBudgetAllocations_TravelRequestId",
                table: "TravelBudgetAllocations",
                column: "TravelRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_LocationId",
                table: "TravelRequests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentGradeId",
                table: "Users",
                column: "CurrentGradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradesHistory");

            migrationBuilder.DropTable(
                name: "TravelBudgetAllocations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TravelRequests");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
