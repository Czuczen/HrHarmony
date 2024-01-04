using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HrHarmony.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    EducationLevelId = table.Column<int>(type: "int", nullable: false),
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AbsenceTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Absences_AbsenceTypes_AbsenceTypeId",
                        column: x => x.AbsenceTypeId,
                        principalTable: "AbsenceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Absences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonuses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Allowances = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZUSContributions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AbsenceTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Urlop wypoczynkowy" },
                    { 2, "Urlop bezpłatny" },
                    { 3, "Urlop okolicznościowy" },
                    { 4, "Urlop zdrowotny" },
                    { 5, "Urlop macierzyński" },
                    { 6, "Urlop ojcowski" },
                    { 7, "Urlop na żądanie" },
                    { 8, "Urlop naukowy" },
                    { 9, "Urlop szkoleniowy" },
                    { 10, "Urlop wychowawczy" },
                    { 11, "Urlop rehabilitacyjny" },
                    { 12, "Zwolnienie lekarskie" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Warszawa", "00-001", "ul. Marszałkowska" },
                    { 2, "Gdańsk", "80-001", "ul. Długa" },
                    { 3, "Kraków", "30-001", "ul. Floriańska" },
                    { 4, "Poznań", "60-001", "ul. Wojska Polskiego" },
                    { 5, "Lublin", "20-001", "ul. Piłsudskiego" },
                    { 6, "Katowice", "40-001", "ul. 3 Maja" },
                    { 7, "Łódź", "90-001", "ul. Mickiewicza" },
                    { 8, "Wrocław", "50-001", "ul. Zamkowa" },
                    { 9, "Szczecin", "70-001", "ul. Słowackiego" },
                    { 10, "Bydgoszcz", "85-001", "ul. Kościuszki" },
                    { 11, "Gdynia", "81-001", "ul. Jana III Sobieskiego" },
                    { 12, "Częstochowa", "42-001", "ul. Świętojańska" },
                    { 13, "Radom", "26-001", "ul. Kopernika" },
                    { 14, "Kielce", "25-001", "ul. 1 Maja" },
                    { 15, "Olsztyn", "10-001", "ul. Sienkiewicza" },
                    { 16, "Rzeszów", "35-001", "ul. Mickiewicza" },
                    { 17, "Białystok", "15-001", "ul. Piastowska" },
                    { 18, "Opole", "45-001", "ul. 11 Listopada" },
                    { 19, "Gorzów Wielkopolski", "66-001", "ul. Malczewskiego" },
                    { 20, "Tarnów", "33-001", "ul. Wyzwolenia" }
                });

            migrationBuilder.InsertData(
                table: "ContractTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Umowa o pracę" },
                    { 2, "Umowa zlecenie" },
                    { 3, "Umowa o dzieło" },
                    { 4, "B2B" }
                });

            migrationBuilder.InsertData(
                table: "EducationLevels",
                columns: new[] { "Id", "LevelName" },
                values: new object[,]
                {
                    { 1, "Podstawowe" },
                    { 2, "Średnie" },
                    { 3, "Wyższe" },
                    { 4, "Inżynierskie" },
                    { 5, "Magisterskie" },
                    { 6, "Doktorat" }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "ExperienceName" },
                values: new object[,]
                {
                    { 1, "Stażysta" },
                    { 2, "Młodszy" },
                    { 3, "Średniozaawansowany" },
                    { 4, "Starszy" },
                    { 5, "Ekspert" }
                });

            migrationBuilder.InsertData(
                table: "MaritalStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Kawaler" },
                    { 2, "Panna" },
                    { 3, "Żonaty" },
                    { 4, "Mężatka" },
                    { 5, "Rozwiedziony" },
                    { 6, "Rozwiedziona" },
                    { 7, "Wdowiec" },
                    { 8, "Wdowa" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AddressId", "DateOfBirth", "EducationLevelId", "Email", "ExperienceId", "FullName", "MaritalStatusId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "janusz.kowalski@example.com", 1, "Janusz Kowalski", 1, "123456789" },
                    { 2, 2, new DateTime(1992, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "anna.nowak@example.com", 2, "Anna Nowak", 2, "987654321" },
                    { 3, 3, new DateTime(1988, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "piotr.wisniewski@example.com", 3, "Piotr Wiśniewski", 3, "111222333" },
                    { 4, 4, new DateTime(1991, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "zofia.dabrowska@example.com", 4, "Zofia Dąbrowska", 4, "444555666" },
                    { 5, 5, new DateTime(1985, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "adam.kaczmarek@example.com", 5, "Adam Kaczmarek", 5, "777888999" }
                });

            migrationBuilder.InsertData(
                table: "Absences",
                columns: new[] { "Id", "AbsenceTypeId", "EmployeeId", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 4, new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, 5, new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 6, 1, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 7, 2, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 8, 3, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 9, 4, new DateTime(2023, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 10, 5, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 11, 1, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 12, 2, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, 3, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 2, 4, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 3, 5, new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EmploymentContracts",
                columns: new[] { "Id", "BasicSalary", "ContractNumber", "ContractTypeId", "EmployeeId", "EndDate", "HourlyRate", "MonthlyRate", "StartDate" },
                values: new object[,]
                {
                    { 1, 36000.0m, "CNT1", 1, 1, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0m, 3000.0m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 33600.0m, "CNT2", 2, 2, null, 45.0m, 2800.0m, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 38400.0m, "CNT3", 3, 3, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.0m, 3200.0m, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 42000.0m, "CNT4", 4, 4, null, 60.0m, 3500.0m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 36000.0m, "CNT5", 1, 5, null, 50.0m, 3000.0m, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "AdditionalSalary", "Allowances", "BasicSalary", "Bonuses", "EmployeeId", "IncomeTax", "PaymentDate", "ZUSContributions" },
                values: new object[,]
                {
                    { 1, 500.0m, 200.0m, 4000.0m, 1000.0m, 1, 1000.0m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 500.0m },
                    { 2, 600.0m, 250.0m, 4500.0m, 1200.0m, 2, 1100.0m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 550.0m },
                    { 3, 550.0m, 225.0m, 4200.0m, 1100.0m, 3, 1050.0m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 525.0m },
                    { 4, 575.0m, 235.0m, 4300.0m, 1150.0m, 4, 1075.0m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 535.0m },
                    { 5, 600.0m, 250.0m, 4400.0m, 1200.0m, 5, 1100.0m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 550.0m },
                    { 6, 625.0m, 260.0m, 4500.0m, 1250.0m, 1, 1125.0m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 560.0m },
                    { 7, 625.0m, 275.0m, 4600.0m, 1300.0m, 2, 1150.0m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 575.0m },
                    { 8, 650.0m, 285.0m, 4700.0m, 1350.0m, 3, 1175.0m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 585.0m },
                    { 9, 675.0m, 300.0m, 4800.0m, 1400.0m, 4, 1200.0m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 600.0m },
                    { 10, 700.0m, 310.0m, 4900.0m, 1450.0m, 5, 1225.0m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 610.0m },
                    { 11, 725.0m, 325.0m, 5000.0m, 1500.0m, 1, 1250.0m, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 625.0m },
                    { 12, 750.0m, 335.0m, 5100.0m, 1550.0m, 2, 1275.0m, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 635.0m },
                    { 13, 775.0m, 350.0m, 5200.0m, 1600.0m, 3, 1300.0m, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 650.0m },
                    { 14, 800.0m, 360.0m, 5300.0m, 1650.0m, 4, 1325.0m, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 660.0m },
                    { 15, 825.0m, 375.0m, 5400.0m, 1700.0m, 5, 1350.0m, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 675.0m },
                    { 16, 850.0m, 385.0m, 5500.0m, 1750.0m, 1, 1375.0m, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 685.0m },
                    { 17, 875.0m, 400.0m, 5600.0m, 1800.0m, 2, 1400.0m, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 700.0m },
                    { 18, 900.0m, 410.0m, 5700.0m, 1850.0m, 3, 1425.0m, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 710.0m },
                    { 19, 925.0m, 425.0m, 5800.0m, 1900.0m, 4, 1450.0m, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 725.0m },
                    { 20, 950.0m, 435.0m, 5900.0m, 1950.0m, 5, 1475.0m, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 735.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absences_AbsenceTypeId",
                table: "Absences",
                column: "AbsenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_EmployeeId",
                table: "Absences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressId",
                table: "Employees",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EducationLevelId",
                table: "Employees",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ExperienceId",
                table: "Employees",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MaritalStatusId",
                table: "Employees",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_ContractTypeId",
                table: "EmploymentContracts",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "EmploymentContracts");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "AbsenceTypes");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "EducationLevels");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");
        }
    }
}
