using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCropEntityData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975576/pexels-photo-17975576.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34387471/pexels-photo-34387471.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/32565438/pexels-photo-32565438.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975576/pexels-photo-17975576.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34940646/pexels-photo-34940646.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/32565438/pexels-photo-32565438.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975576/pexels-photo-17975576.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://ofoodiuk.com/wp-content/uploads/2024/11/Ewedu-Jute-Leaves-Photoroom1.webp", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/16732700/pexels-photo-16732700.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://ofoodiuk.com/wp-content/uploads/2024/11/Ewedu-Jute-Leaves-Photoroom1.webp", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/16732700/pexels-photo-16732700.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://ofoodiuk.com/wp-content/uploads/2024/11/Ewedu-Jute-Leaves-Photoroom1.webp", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/16732700/pexels-photo-16732700.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34387471/pexels-photo-34387471.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/33384524/pexels-photo-33384524.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34387471/pexels-photo-34387471.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/34284738/pexels-photo-34284738.jpeg", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/16732700/pexels-photo-16732700.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://ofoodiuk.com/wp-content/uploads/2024/11/Ewedu-Jute-Leaves-Photoroom1.webp", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/16732700/pexels-photo-16732700.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13551695/pexels-photo-13551695.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/31848991/pexels-photo-31848991.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://ofoodiuk.com/wp-content/uploads/2024/11/Ewedu-Jute-Leaves-Photoroom1.webp", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13156639/pexels-photo-13156639.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/17975573/pexels-photo-17975573.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/19787804/pexels-photo-19787804.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/12191414/pexels-photo-12191414.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/16732700/pexels-photo-16732700.jpeg", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/20180750/pexels-photo-20180750.jpeg", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.pexels.com/photos/13551695/pexels-photo-13551695.jpeg", new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://ofoodiuk.com/wp-content/uploads/2024/11/Ewedu-Jute-Leaves-Photoroom1.webp", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1533758313437-080353c7a6e1?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1515942661900-94b3d197c591?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1533758313437-080353c7a6e1?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", new List<string> { "Open Field" }, new List<string> { "Rainfed", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1589984662646-e7b2e4962f18?w=500", new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1571575173700-afb9492e6a50?w=500", new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", new List<string> { "Open Field" }, new List<string> { "Drip", "Sprinkler" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", new List<string> { "Greenhouse", "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1589984662646-e7b2e4962f18?w=500", new List<string> { "Open Field" }, new List<string> { "Drip" } });

            migrationBuilder.UpdateData(
                table: "Crops",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ImageUrl", "SupportedEnvironmentTypes", "SupportedIrrigationMethods" },
                values: new object[] { "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", new List<string> { "Open Field", "Greenhouse" }, new List<string> { "Drip", "Sprinkler" } });
        }
    }
}
