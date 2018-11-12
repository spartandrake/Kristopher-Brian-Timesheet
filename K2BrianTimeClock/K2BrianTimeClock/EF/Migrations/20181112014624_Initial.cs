using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace K2BrianTimeClock.DAL.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TimeClock");

            migrationBuilder.CreateTable(
                name: "DateAndTimes",
                schema: "TimeClock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Minute = table.Column<int>(nullable: false),
                    Hour = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateAndTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClockIns",
                schema: "TimeClock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TimeInId = table.Column<int>(nullable: true),
                    TimeOutId = table.Column<int>(nullable: true),
                    HoursWorked = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    TimeSheetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClockIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClockIns_DateAndTimes_TimeInId",
                        column: x => x.TimeInId,
                        principalSchema: "TimeClock",
                        principalTable: "DateAndTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClockIns_DateAndTimes_TimeOutId",
                        column: x => x.TimeOutId,
                        principalSchema: "TimeClock",
                        principalTable: "DateAndTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                schema: "TimeClock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    HoursWorked = table.Column<int>(nullable: false),
                    TotalPay = table.Column<decimal>(type: "money", nullable: false, computedColumnSql: "[HoursWorked]*[CurrentWage]"),
                    EmployeeId = table.Column<int>(nullable: true),
                    CurrentWage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "TimeClock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    EmployeeFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    EmployeeLastName = table.Column<string>(maxLength: 50, nullable: true),
                    CurrentWage = table.Column<decimal>(type: "money", maxLength: 4, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsExempt = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<int>(nullable: true),
                    TimeSheetId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "TimeClock",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_TimeSheets_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalSchema: "TimeClock",
                        principalTable: "TimeSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClockIns_EmployeeId",
                schema: "TimeClock",
                table: "ClockIns",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClockIns_TimeInId",
                schema: "TimeClock",
                table: "ClockIns",
                column: "TimeInId");

            migrationBuilder.CreateIndex(
                name: "IX_ClockIns_TimeOutId",
                schema: "TimeClock",
                table: "ClockIns",
                column: "TimeOutId");

            migrationBuilder.CreateIndex(
                name: "IX_ClockIns_TimeSheetId",
                schema: "TimeClock",
                table: "ClockIns",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees",
                schema: "TimeClock",
                table: "Employees",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                schema: "TimeClock",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TimeSheetId",
                schema: "TimeClock",
                table: "Employees",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_EmployeeId",
                schema: "TimeClock",
                table: "TimeSheets",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClockIns_Employees_EmployeeId",
                schema: "TimeClock",
                table: "ClockIns",
                column: "EmployeeId",
                principalSchema: "TimeClock",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClockIns_TimeSheets_TimeSheetId",
                schema: "TimeClock",
                table: "ClockIns",
                column: "TimeSheetId",
                principalSchema: "TimeClock",
                principalTable: "TimeSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheets_Employees_EmployeeId",
                schema: "TimeClock",
                table: "TimeSheets",
                column: "EmployeeId",
                principalSchema: "TimeClock",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheets_Employees_EmployeeId",
                schema: "TimeClock",
                table: "TimeSheets");

            migrationBuilder.DropTable(
                name: "ClockIns",
                schema: "TimeClock");

            migrationBuilder.DropTable(
                name: "DateAndTimes",
                schema: "TimeClock");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "TimeClock");

            migrationBuilder.DropTable(
                name: "TimeSheets",
                schema: "TimeClock");
        }
    }
}
