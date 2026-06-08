using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketPalce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FarmerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeedingCycleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CropId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HarvestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    QuantityAvailable = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductListings_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductListings_FarmerProfiles_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "FarmerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductListings_SeedingCycles_SeedingCycleId",
                        column: x => x.SeedingCycleId,
                        principalTable: "SeedingCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TraderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceAtPurchase = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryNotes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrders_ProductListings_ProductListingId",
                        column: x => x.ProductListingId,
                        principalTable: "ProductListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOrders_TraderProfiles_TraderId",
                        column: x => x.TraderId,
                        principalTable: "TraderProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FarmerRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewerTraderId = table.Column<Guid>(type: "uuid", nullable: false),
                    FarmerId = table.Column<Guid>(type: "uuid", nullable: false),
                    RatingValue = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmerRatings_FarmerProfiles_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "FarmerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FarmerRatings_ProductOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ProductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmerRatings_TraderProfiles_ReviewerTraderId",
                        column: x => x.ReviewerTraderId,
                        principalTable: "TraderProfiles",
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
                name: "IX_FarmerRatings_FarmerId",
                table: "FarmerRatings",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerRatings_OrderId",
                table: "FarmerRatings",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FarmerRatings_ReviewerTraderId",
                table: "FarmerRatings",
                column: "ReviewerTraderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListings_CropId",
                table: "ProductListings",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListings_FarmerId",
                table: "ProductListings",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListings_SeedingCycleId",
                table: "ProductListings",
                column: "SeedingCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductListingId",
                table: "ProductOrders",
                column: "ProductListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_TraderId",
                table: "ProductOrders",
                column: "TraderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmerRatings");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "ProductListings");

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
