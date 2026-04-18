using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCropEntityConfigurationClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WaterAvailability = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ResetCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ResetCodeExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GovernorateId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DaysToHarvest = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Season = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FarmingMethod = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PlantingTime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    HarvestTime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MinTemperature = table.Column<int>(type: "integer", nullable: false),
                    MaxTemperature = table.Column<int>(type: "integer", nullable: false),
                    SupportedIrrigationMethods = table.Column<List<string>>(type: "text[]", nullable: false),
                    SupportedEnvironmentTypes = table.Column<List<string>>(type: "text[]", nullable: false),
                    IrrigationLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WaterRequirementCategory = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MinProductionPerDonum = table.Column<double>(type: "double precision", nullable: false),
                    MaxProductionPerDonum = table.Column<double>(type: "double precision", nullable: false),
                    MinExpectedPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MaxExpectedPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MinEstimatedCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MaxEstimatedCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crops_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CvUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: true),
                    GovernorateId = table.Column<int>(type: "integer", nullable: true),
                    AddressDetail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Specialization = table.Column<int>(type: "integer", nullable: true),
                    CanVisitOnSite = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertProfiles_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FarmerId = table.Column<Guid>(type: "uuid", nullable: false),
                    GovernorateId = table.Column<int>(type: "integer", nullable: false),
                    SoilTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Area = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false),
                    WaterSourceType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    WaterAvailability = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EnvironmentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IrrigationMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lands_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lands_SoilTypes_SoilTypeId",
                        column: x => x.SoilTypeId,
                        principalTable: "SoilTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lands_Users_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CropSoilTypes",
                columns: table => new
                {
                    CropId = table.Column<int>(type: "integer", nullable: false),
                    SoilTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropSoilTypes", x => new { x.CropId, x.SoilTypeId });
                    table.ForeignKey(
                        name: "FK_CropSoilTypes_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CropSoilTypes_SoilTypes_SoilTypeId",
                        column: x => x.SoilTypeId,
                        principalTable: "SoilTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpertProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertAvailabilities_ExpertProfiles_ExpertProfileId",
                        column: x => x.ExpertProfileId,
                        principalTable: "ExpertProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "Name", "WaterAvailability" },
                values: new object[,]
                {
                    { 1, "Jerusalem", "Low" },
                    { 2, "Ramallah", "Medium" },
                    { 3, "Hebron", "Low" },
                    { 4, "Bethlehem", "Low to Medium" },
                    { 5, "Nablus", "Medium to High" },
                    { 6, "Jenin", "High" },
                    { 7, "Tulkarm", "Medium" },
                    { 8, "Qalqilya", "High" },
                    { 9, "Salfit", "Medium" },
                    { 10, "Tubas", "Medium" },
                    { 11, "Jericho", "Low" },
                    { 12, "Gaza", "Very Low" }
                });

            migrationBuilder.InsertData(
                table: "SoilTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Red Soil" },
                    { 2, "Rendzina Soil" },
                    { 3, "Clay Soil" },
                    { 4, "Sandy Soil" }
                });

            migrationBuilder.InsertData(
                table: "Crops",
                columns: new[] { "Id", "Category", "DaysToHarvest", "FarmingMethod", "GovernorateId", "HarvestTime", "ImageUrl", "IrrigationLevel", "MaxEstimatedCost", "MaxExpectedPrice", "MaxProductionPerDonum", "MaxTemperature", "MinEstimatedCost", "MinExpectedPrice", "MinProductionPerDonum", "MinTemperature", "Name", "PlantingTime", "Season", "SupportedEnvironmentTypes", "SupportedIrrigationMethods", "WaterRequirementCategory" },
                values: new object[,]
                {
                    { 1, "Field Crops", 190, "Open Field", 1, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.5, 20, 500m, 2.0m, 0.29999999999999999, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 2, "Vegetables", 110, "Open Field", 1, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 3, "Vegetables", 60, "Open Field", 1, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 4, "Vegetables", 90, "Open Field", 1, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 6.0, 30, 2000m, 2.0m, 4.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 5, "Vegetables", 60, "Open Field", 1, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 7.0, 30, 1200m, 3.0m, 5.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 6, "Vegetables", 80, "Greenhouse", 1, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 7.0, 30, 2500m, 6.0m, 5.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 7, "Vegetables", 50, "Open Field", 1, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 6.0, 28, 1200m, 4.0m, 4.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 8, "Vegetables", 100, "Open Field", 1, "1/3–30/4", "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", "40–60", 1200m, 8.0m, 2.0, 20, 800m, 5.0m, 1.0, 10, "Peas", "1/11–15/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" }, "Low" },
                    { 9, "Field Crops", 190, "Open Field", 2, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.59999999999999998, 20, 500m, 2.0m, 0.40000000000000002, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 10, "Vegetables", 110, "Open Field", 2, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 3.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 11, "Vegetables", 60, "Open Field", 2, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 12, "Vegetables", 90, "Open Field", 2, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 6.0, 30, 2000m, 2.0m, 4.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 13, "Vegetables", 60, "Open Field", 2, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 7.0, 30, 1200m, 3.0m, 5.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 14, "Vegetables", 80, "Greenhouse", 2, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 7.0, 30, 2500m, 6.0m, 5.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 15, "Vegetables", 50, "Open Field", 2, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 6.0, 28, 1200m, 4.0m, 4.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 16, "Vegetables", 55, "Open Field", 2, "1/5–30/6", "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", "70–90", 1500m, 9.0m, 2.5, 30, 1000m, 5.0m, 1.5, 20, "Green Beans", "1/3–1/4", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "Medium" },
                    { 17, "Field Crops", 190, "Open Field", 3, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.5, 20, 500m, 2.0m, 0.29999999999999999, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 18, "Field Crops", 180, "Open Field", 3, "1/5–30/6", "https://images.unsplash.com/photo-1533758313437-080353c7a6e1?w=500", "0–10", 800m, 2.0m, 0.5, 20, 400m, 1.5m, 0.29999999999999999, 10, "Barley", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 19, "Vegetables", 110, "Open Field", 3, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 3.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 20, "Vegetables", 90, "Open Field", 3, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 6.0, 30, 2000m, 2.0m, 4.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 21, "Vegetables", 60, "Open Field", 3, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 7.0, 30, 1200m, 3.0m, 5.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 22, "Vegetables", 50, "Open Field", 3, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 6.0, 28, 1200m, 4.0m, 4.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 23, "Vegetables", 100, "Open Field", 3, "1/3–30/4", "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", "40–60", 1200m, 8.0m, 2.0, 20, 800m, 5.0m, 1.0, 10, "Peas", "1/11–15/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" }, "Low" },
                    { 24, "Field Crops", 160, "Open Field", 3, "1/5–30/6", "https://images.unsplash.com/photo-1515942661900-94b3d197c591?w=500", "0–10", 700m, 6.0m, 0.40000000000000002, 20, 400m, 4.0m, 0.20000000000000001, 10, "Lentils", "1/11–15/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed" }, "Very Low" },
                    { 25, "Field Crops", 190, "Open Field", 4, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.5, 20, 500m, 2.0m, 0.29999999999999999, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 26, "Field Crops", 180, "Open Field", 4, "1/5–30/6", "https://images.unsplash.com/photo-1533758313437-080353c7a6e1?w=500", "0–10", 800m, 2.0m, 0.5, 20, 400m, 1.5m, 0.29999999999999999, 10, "Barley", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 27, "Vegetables", 110, "Open Field", 4, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "70–90", 1200m, 3.0m, 4.0, 22, 800m, 2.0m, 3.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 28, "Vegetables", 60, "Open Field", 4, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–70", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 29, "Vegetables", 90, "Open Field", 4, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–120", 3000m, 4.0m, 6.0, 30, 2000m, 2.0m, 4.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 30, "Vegetables", 60, "Open Field", 4, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–110", 1800m, 6.0m, 7.0, 30, 1200m, 3.0m, 5.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 31, "Vegetables", 50, "Open Field", 4, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "80–100", 1800m, 7.0m, 6.0, 28, 1200m, 4.0m, 4.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "Medium" },
                    { 32, "Vegetables", 100, "Open Field", 4, "1/3–30/4", "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", "40–60", 1200m, 8.0m, 2.0, 20, 800m, 5.0m, 1.0, 10, "Peas", "1/11–15/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" }, "Low" },
                    { 33, "Field Crops", 190, "Open Field", 5, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.59999999999999998, 20, 500m, 2.0m, 0.40000000000000002, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 34, "Vegetables", 110, "Open Field", 5, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 35, "Vegetables", 60, "Open Field", 5, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 36, "Vegetables", 90, "Open Field", 5, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 7.0, 30, 2000m, 2.0m, 5.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 37, "Vegetables", 60, "Open Field", 5, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 8.0, 30, 1200m, 3.0m, 6.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 38, "Vegetables", 80, "Greenhouse", 5, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 8.0, 30, 2500m, 6.0m, 6.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 39, "Vegetables", 50, "Open Field", 5, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 7.0, 28, 1200m, 4.0m, 5.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 40, "Leafy Greens", 75, "Open Field", 5, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 3.0, 35, 800m, 5.0m, 2.0, 25, "Molokhia", "15/3–30/4", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 41, "Field Crops", 190, "Open Field", 6, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.59999999999999998, 20, 500m, 2.0m, 0.40000000000000002, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 42, "Vegetables", 110, "Open Field", 6, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 6.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 43, "Vegetables", 90, "Open Field", 6, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–140", 3000m, 4.0m, 8.0, 30, 2000m, 2.0m, 6.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 44, "Vegetables", 60, "Open Field", 6, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 8.0, 30, 1200m, 3.0m, 6.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 45, "Vegetables", 80, "Greenhouse", 6, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 8.0, 30, 2500m, 6.0m, 6.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 46, "Vegetables", 50, "Open Field", 6, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 7.0, 28, 1200m, 4.0m, 5.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 47, "Vegetables", 100, "Greenhouse", 6, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 9.0, 30, 2500m, 3.0m, 7.0, 22, "Eggplant", "1/2–15/3", "Spring", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 48, "Leafy Greens", 75, "Open Field", 6, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 3.0, 35, 800m, 5.0m, 2.0, 25, "Molokhia", "15/3–30/4", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 49, "Vegetables", 60, "Open Field", 7, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 8.0, 30, 1200m, 3.0m, 6.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 50, "Vegetables", 90, "Open Field", 7, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–140", 3000m, 4.0m, 7.0, 30, 2000m, 2.0m, 5.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 51, "Vegetables", 80, "Greenhouse", 7, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 7.0, 30, 2500m, 6.0m, 5.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 52, "Vegetables", 50, "Open Field", 7, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 7.0, 28, 1200m, 4.0m, 5.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 53, "Vegetables", 100, "Greenhouse", 7, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 9.0, 30, 2500m, 3.0m, 7.0, 22, "Eggplant", "1/2–15/3", "Spring", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 54, "Vegetables", 110, "Open Field", 7, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 55, "Vegetables", 60, "Open Field", 7, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 56, "Leafy Greens", 75, "Open Field", 7, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 3.0, 35, 800m, 5.0m, 2.0, 25, "Molokhia", "15/3–30/4", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 57, "Vegetables", 60, "Open Field", 8, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "80–110", 1800m, 7.0m, 8.0, 30, 1200m, 4.0m, 6.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 58, "Vegetables", 90, "Open Field", 8, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 5.0m, 7.0, 30, 2000m, 3.0m, 5.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 59, "Vegetables", 80, "Greenhouse", 8, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 7.0, 30, 2500m, 6.0m, 5.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 60, "Vegetables", 50, "Open Field", 8, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 7.0, 28, 1200m, 4.0m, 5.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 61, "Vegetables", 110, "Open Field", 8, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 62, "Vegetables", 60, "Open Field", 8, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 63, "Vegetables", 100, "Greenhouse", 8, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 9.0, 30, 2500m, 3.0m, 7.0, 22, "Eggplant", "1/2–15/3", "Spring", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 64, "Vegetables", 55, "Open Field", 8, "1/5–30/6", "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", "70–90", 1500m, 9.0m, 2.5, 30, 1000m, 5.0m, 1.5, 20, "Green Beans", "1/3–1/4", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "Medium" },
                    { 65, "Field Crops", 190, "Open Field", 9, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.59999999999999998, 20, 500m, 2.0m, 0.40000000000000002, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 66, "Vegetables", 110, "Open Field", 9, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 5.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 67, "Vegetables", 60, "Open Field", 9, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 3.0, 20, 600m, 4.0m, 2.0, 10, "Lettuce", "1/10–20/10", "Winter", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 68, "Vegetables", 90, "Open Field", 9, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 7.0, 30, 2000m, 2.0m, 5.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 69, "Vegetables", 60, "Open Field", 9, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 7.0, 30, 1200m, 3.0m, 5.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 70, "Vegetables", 80, "Greenhouse", 9, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 7.0, 30, 2500m, 6.0m, 5.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 71, "Vegetables", 50, "Open Field", 9, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 6.0, 28, 1200m, 4.0m, 4.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 72, "Vegetables", 55, "Open Field", 9, "1/5–30/6", "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", "70–90", 1500m, 9.0m, 2.5, 30, 1000m, 5.0m, 1.5, 20, "Green Beans", "1/3–1/4", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "Medium" },
                    { 73, "Field Crops", 190, "Open Field", 10, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 0.59999999999999998, 20, 500m, 2.0m, 0.40000000000000002, 10, "Wheat", "15/11–30/12", "Winter", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" }, "Very Low" },
                    { 74, "Vegetables", 110, "Open Field", 10, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 6.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Winter", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 75, "Vegetables", 90, "Open Field", 10, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–140", 3000m, 4.0m, 8.0, 30, 2000m, 2.0m, 6.0, 22, "Tomato", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 76, "Vegetables", 60, "Open Field", 10, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 8.0, 30, 1200m, 3.0m, 6.0, 22, "Cucumber", "1/3–15/4", "Summer", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 77, "Vegetables", 80, "Greenhouse", 10, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 8.0, 30, 2500m, 6.0m, 6.0, 20, "Sweet Pepper", "15/2–15/3", "Summer", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 78, "Vegetables", 50, "Open Field", 10, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 7.0, 28, 1200m, 4.0m, 5.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 79, "Vegetables", 100, "Greenhouse", 10, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 9.0, 30, 2500m, 3.0m, 7.0, 22, "Eggplant", "1/2–15/3", "Spring", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 80, "Leafy Greens", 75, "Open Field", 10, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 3.0, 35, 800m, 5.0m, 2.0, 25, "Molokhia", "15/3–30/4", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 81, "Vegetables", 65, "Greenhouse", 11, "1/11–1/4", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "120", 6000m, 4.0m, 20.0, 25, 5000m, 3.0m, 18.0, 20, "Tomato", "25/8–10/9", "Winter", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 82, "Vegetables", 90, "Greenhouse", 11, "1/12–30/3", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "100–120", 5000m, 5.0m, 12.0, 25, 3500m, 3.0m, 10.0, 18, "Cucumber", "1/9–15/10", "Winter", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 83, "Vegetables", 75, "Greenhouse", 11, "1/12–30/4", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "110–130", 4500m, 12.0m, 9.0, 30, 3500m, 6.0m, 7.0, 20, "Sweet Pepper", "15/9–15/10", "Winter", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 84, "Vegetables", 60, "Open Field", 11, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 8.0, 28, 1200m, 4.0m, 6.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 85, "Vegetables", 100, "Greenhouse", 11, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "110–130", 3500m, 6.0m, 10.0, 30, 2500m, 3.0m, 8.0, 22, "Eggplant", "1/2–15/3", "Spring", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 86, "Fruits", 90, "Open Field", 11, "1/5–15/6", "https://images.unsplash.com/photo-1589984662646-e7b2e4962f18?w=500", "80–100", 2500m, 4.0m, 6.0, 35, 1500m, 2.0m, 4.0, 25, "Watermelon", "1/2–1/3", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip" }, "Medium" },
                    { 87, "Fruits", 90, "Open Field", 11, "1/5–15/6", "https://images.unsplash.com/photo-1571575173700-afb9492e6a50?w=500", "80–100", 2000m, 4.0m, 5.0, 35, 1200m, 2.0m, 3.0, 25, "Melon", "1/2–1/3", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip" }, "Medium" },
                    { 88, "Leafy Greens", 75, "Open Field", 11, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 10.0m, 3.0, 35, 800m, 5.0m, 2.0, 25, "Molokhia", "15/3–30/4", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 89, "Vegetables", 90, "Greenhouse", 12, "1/12–30/3", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "120–150", 6000m, 5.0m, 22.0, 25, 5000m, 3.0m, 15.0, 18, "Tomato", "1/9–15/10", "Winter", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 90, "Vegetables", 90, "Greenhouse", 12, "1/12–30/3", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 5000m, 5.0m, 12.0, 25, 3500m, 3.0m, 8.0, 18, "Cucumber", "1/9–15/10", "Winter", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 91, "Vegetables", 75, "Greenhouse", 12, "1/12–30/4", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 4000m, 10.0m, 8.0, 28, 3000m, 6.0m, 6.0, 20, "Sweet Pepper", "15/9–15/10", "Winter", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 92, "Vegetables", 120, "Open Field", 12, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 6.0, 22, 800m, 2.0m, 4.0, 15, "Potato", "15/10–30/11", "Spring", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" },
                    { 93, "Vegetables", 100, "Greenhouse", 12, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 9.0, 30, 2500m, 3.0m, 7.0, 22, "Eggplant", "1/2–15/3", "Spring", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" }, "High" },
                    { 94, "Vegetables", 60, "Open Field", 12, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 7.0, 28, 1200m, 4.0m, 5.0, 18, "Zucchini", "15/2–15/3", "Spring", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" }, "High" },
                    { 95, "Fruits", 90, "Open Field", 12, "1/5–15/6", "https://images.unsplash.com/photo-1589984662646-e7b2e4962f18?w=500", "80–100", 2500m, 4.0m, 6.0, 35, 1500m, 2.0m, 4.0, 25, "Watermelon", "1/2–1/3", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip" }, "Medium" },
                    { 96, "Leafy Greens", 75, "Open Field", 12, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 3.0, 35, 800m, 5.0m, 2.0, 25, "Molokhia", "15/3–30/4", "Summer", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" }, "Medium" }
                });

            migrationBuilder.InsertData(
                table: "CropSoilTypes",
                columns: new[] { "CropId", "SoilTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 4 },
                    { 4, 1 },
                    { 4, 3 },
                    { 5, 1 },
                    { 5, 4 },
                    { 6, 1 },
                    { 6, 4 },
                    { 7, 1 },
                    { 7, 4 },
                    { 8, 1 },
                    { 8, 2 },
                    { 9, 1 },
                    { 9, 3 },
                    { 10, 2 },
                    { 10, 4 },
                    { 11, 1 },
                    { 11, 4 },
                    { 12, 1 },
                    { 12, 3 },
                    { 13, 1 },
                    { 13, 4 },
                    { 14, 1 },
                    { 14, 4 },
                    { 15, 1 },
                    { 15, 4 },
                    { 16, 1 },
                    { 16, 4 },
                    { 17, 1 },
                    { 17, 3 },
                    { 18, 1 },
                    { 18, 2 },
                    { 18, 3 },
                    { 19, 2 },
                    { 19, 4 },
                    { 20, 1 },
                    { 20, 3 },
                    { 21, 1 },
                    { 21, 4 },
                    { 22, 1 },
                    { 22, 4 },
                    { 23, 1 },
                    { 23, 2 },
                    { 24, 1 },
                    { 24, 2 },
                    { 25, 1 },
                    { 25, 3 },
                    { 26, 1 },
                    { 26, 2 },
                    { 26, 3 },
                    { 27, 2 },
                    { 27, 4 },
                    { 28, 1 },
                    { 28, 4 },
                    { 29, 1 },
                    { 29, 3 },
                    { 30, 1 },
                    { 30, 4 },
                    { 31, 1 },
                    { 31, 4 },
                    { 32, 1 },
                    { 32, 2 },
                    { 33, 1 },
                    { 33, 3 },
                    { 34, 2 },
                    { 34, 4 },
                    { 35, 1 },
                    { 35, 4 },
                    { 36, 1 },
                    { 36, 3 },
                    { 37, 1 },
                    { 37, 4 },
                    { 38, 1 },
                    { 38, 4 },
                    { 39, 1 },
                    { 39, 4 },
                    { 40, 1 },
                    { 40, 3 },
                    { 41, 1 },
                    { 41, 3 },
                    { 42, 2 },
                    { 42, 4 },
                    { 43, 1 },
                    { 43, 3 },
                    { 44, 1 },
                    { 44, 4 },
                    { 45, 1 },
                    { 45, 4 },
                    { 46, 1 },
                    { 46, 4 },
                    { 47, 1 },
                    { 47, 4 },
                    { 48, 1 },
                    { 48, 3 },
                    { 49, 1 },
                    { 49, 4 },
                    { 50, 1 },
                    { 50, 3 },
                    { 51, 1 },
                    { 51, 4 },
                    { 52, 1 },
                    { 52, 4 },
                    { 53, 1 },
                    { 53, 4 },
                    { 54, 2 },
                    { 54, 4 },
                    { 55, 1 },
                    { 55, 4 },
                    { 56, 1 },
                    { 56, 3 },
                    { 57, 1 },
                    { 57, 4 },
                    { 58, 1 },
                    { 58, 3 },
                    { 59, 1 },
                    { 59, 4 },
                    { 60, 1 },
                    { 60, 4 },
                    { 61, 2 },
                    { 61, 4 },
                    { 62, 1 },
                    { 62, 4 },
                    { 63, 1 },
                    { 63, 4 },
                    { 64, 1 },
                    { 64, 4 },
                    { 65, 1 },
                    { 65, 3 },
                    { 66, 1 },
                    { 66, 2 },
                    { 66, 3 },
                    { 67, 2 },
                    { 67, 4 },
                    { 68, 1 },
                    { 68, 4 },
                    { 69, 1 },
                    { 69, 3 },
                    { 70, 1 },
                    { 70, 4 },
                    { 71, 1 },
                    { 71, 4 },
                    { 72, 1 },
                    { 72, 2 },
                    { 73, 1 },
                    { 73, 3 },
                    { 74, 2 },
                    { 74, 4 },
                    { 75, 1 },
                    { 75, 3 },
                    { 76, 1 },
                    { 76, 4 },
                    { 77, 1 },
                    { 77, 4 },
                    { 78, 1 },
                    { 78, 4 },
                    { 79, 1 },
                    { 79, 4 },
                    { 80, 1 },
                    { 80, 3 },
                    { 81, 1 },
                    { 81, 3 },
                    { 82, 1 },
                    { 82, 4 },
                    { 83, 1 },
                    { 83, 4 },
                    { 84, 1 },
                    { 84, 4 },
                    { 85, 1 },
                    { 85, 4 },
                    { 86, 4 },
                    { 87, 4 },
                    { 88, 1 },
                    { 88, 3 },
                    { 89, 1 },
                    { 89, 3 },
                    { 90, 1 },
                    { 90, 4 },
                    { 91, 1 },
                    { 91, 4 },
                    { 92, 2 },
                    { 92, 4 },
                    { 93, 1 },
                    { 93, 4 },
                    { 94, 1 },
                    { 94, 4 },
                    { 95, 4 },
                    { 96, 1 },
                    { 96, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_GovernorateId",
                table: "Crops",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_CropSoilTypes_SoilTypeId",
                table: "CropSoilTypes",
                column: "SoilTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAvailabilities_ExpertProfileId",
                table: "ExpertAvailabilities",
                column: "ExpertProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProfiles_GovernorateId",
                table: "ExpertProfiles",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Lands_FarmerId",
                table: "Lands",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lands_GovernorateId",
                table: "Lands",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Lands_SoilTypeId",
                table: "Lands",
                column: "SoilTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CropSoilTypes");

            migrationBuilder.DropTable(
                name: "ExpertAvailabilities");

            migrationBuilder.DropTable(
                name: "Lands");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "ExpertProfiles");

            migrationBuilder.DropTable(
                name: "SoilTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Governorates");
        }
    }
}
