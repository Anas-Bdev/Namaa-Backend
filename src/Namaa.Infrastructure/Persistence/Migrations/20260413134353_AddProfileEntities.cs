using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FarmerProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    GovernorateId = table.Column<int>(type: "integer", nullable: false),
                    AddressDetail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmerProfiles_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvestorProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    OrganizationName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GovernorateId = table.Column<int>(type: "integer", nullable: false),
                    AddressDetail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestorProfiles_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeedingCycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LandId = table.Column<Guid>(type: "uuid", nullable: false),
                    CropId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstimatedHarvestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualHarvestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SeedQuantity = table.Column<double>(type: "double precision", nullable: false),
                    SeedingArea = table.Column<double>(type: "double precision", nullable: false),
                    ExpectedYield = table.Column<double>(type: "double precision", nullable: false),
                    ActualYield = table.Column<double>(type: "double precision", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedingCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeedingCycles_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeedingCycles_Lands_LandId",
                        column: x => x.LandId,
                        principalTable: "Lands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraderProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BusinessType = table.Column<string>(type: "text", nullable: false),
                    GovernorateId = table.Column<int>(type: "integer", nullable: false),
                    AddressDetail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraderProfiles_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.CreateIndex(
                name: "IX_FarmerProfiles_GovernorateId",
                table: "FarmerProfiles",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorProfiles_GovernorateId",
                table: "InvestorProfiles",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedingCycles_CropId",
                table: "SeedingCycles",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedingCycles_LandId",
                table: "SeedingCycles",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderProfiles_GovernorateId",
                table: "TraderProfiles",
                column: "GovernorateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmerProfiles");

            migrationBuilder.DropTable(
                name: "InvestorProfiles");

            migrationBuilder.DropTable(
                name: "SeedingCycles");

            migrationBuilder.DropTable(
                name: "TraderProfiles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "FullName");

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });
        }
    }
}
