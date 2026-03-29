using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCropEntityAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoilId",
                table: "Lands",
                newName: "SoilTypeId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Lands",
                newName: "GovernorateId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "ExpertProfiles",
                newName: "GovernorateId");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "ExpertAvailabilities",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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
                    IrrigationLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WaterRequirementCategory = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
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
                columns: new[] { "Id", "Category", "DaysToHarvest", "FarmingMethod", "GovernorateId", "HarvestTime", "ImageUrl", "IrrigationLevel", "MaxEstimatedCost", "MaxExpectedPrice", "MaxTemperature", "MinEstimatedCost", "MinExpectedPrice", "MinTemperature", "Name", "PlantingTime", "Season", "WaterRequirementCategory" },
                values: new object[,]
                {
                    { 1, "Vegetables", 65, "Greenhouse", 11, "1/11–1/4", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "120", 6000m, 4.0m, 25, 5000m, 3.0m, 20, "Tomato", "25/8–10/9", "Winter", "High" },
                    { 2, "Vegetables", 90, "Greenhouse", 11, "1/12–30/3", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "100–120", 5000m, 5.0m, 25, 3500m, 3.0m, 18, "Cucumber", "1/9–15/10", "Winter", "High" },
                    { 3, "Vegetables", 75, "Greenhouse", 11, "1/12–30/4", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "110–130", 4500m, 12.0m, 30, 3500m, 6.0m, 20, "Sweet Pepper", "15/9–15/10", "Winter", "High" },
                    { 4, "Vegetables", 60, "Open Field", 11, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 5, "Vegetables", 100, "Greenhouse", 11, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "110–130", 3500m, 6.0m, 30, 2500m, 3.0m, 22, "Eggplant", "1/2–15/3", "Spring", "High" },
                    { 6, "Fruits", 90, "Open Field", 11, "1/5–15/6", "https://images.unsplash.com/photo-1589984662646-e7b2e4962f18?w=500", "80–100", 2500m, 4.0m, 35, 1500m, 2.0m, 25, "Watermelon", "1/2–1/3", "Summer", "Medium" },
                    { 7, "Fruits", 90, "Open Field", 11, "1/5-15/6", "https://images.unsplash.com/photo-1571575173700-afb9492e6a50?w=500", "80-100", 2000m, 4.0m, 35, 1200m, 2.0m, 25, "Melon", "1/2-1/3", "Summer", "Medium" },
                    { 8, "Leafy Greens", 75, "Open Field", 11, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 10.0m, 35, 800m, 5.0m, 25, "Molokhia", "15/3–30/4", "Summer", "Medium" },
                    { 9, "Vegetables", 60, "Open Field", 7, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 10, "Vegetables", 90, "Open Field", 7, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–140", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 11, "Vegetables", 80, "Greenhouse", 7, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 12, "Vegetables", 50, "Open Field", 7, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 13, "Vegetables", 100, "Greenhouse", 7, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 30, 2500m, 3.0m, 22, "Eggplant", "1/2–15/3", "Spring", "High" },
                    { 14, "Vegetables", 110, "Open Field", 7, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 15, "Vegetables", 60, "Open Field", 7, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 16, "Leafy Greens", 75, "Open Field", 7, "1/6-30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60-80", 1200m, 9.0m, 35, 800m, 5.0m, 25, "Molokhia", "15/3–30/4", "Summer", "Medium" },
                    { 17, "Vegetables", 60, "Open Field", 8, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "80–110", 1800m, 7.0m, 30, 1200m, 4.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 18, "Vegetables", 90, "Open Field", 8, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 5.0m, 30, 2000m, 3.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 19, "Vegetables", 80, "Greenhouse", 8, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 20, "Vegetables", 50, "Open Field", 8, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 21, "Vegetables", 110, "Open Field", 8, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 22, "Vegetables", 60, "Open Field", 8, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 23, "Vegetables", 100, "Greenhouse", 8, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 30, 2500m, 3.0m, 22, "Eggplant", "1/2–15/3", "Spring", "High" },
                    { 24, "Vegetables", 55, "Open Field", 8, "1/5–30/6", "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", "70–90", 1500m, 9.0m, 30, 1000m, 5.0m, 20, "Green Beans", "1/3–1/4", "Spring", "Medium" },
                    { 25, "Field Crops", 190, "Open Field", 6, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 26, "Vegetables", 110, "Open Field", 6, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 27, "Vegetables", 95, "Open Field", 6, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–140", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 28, "Vegetables", 60, "Open Field", 6, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 29, "Vegetables", 85, "Greenhouse", 6, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 30, "Vegetables", 50, "Open Field", 6, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 31, "Vegetables", 110, "Greenhouse", 6, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 30, 2500m, 3.0m, 22, "Eggplant", "1/2–15/3", "Spring", "High" },
                    { 32, "Leafy Greens", 75, "Open Field", 6, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 35, 800m, 5.0m, 25, "Molokhia", "15/3–30/4", "Summer", "Medium" },
                    { 33, "Field Crops", 190, "Open Field", 5, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 34, "Vegetables", 110, "Open Field", 5, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 35, "Vegetables", 60, "Open Field", 5, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 36, "Vegetables", 95, "Open Field", 5, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 37, "Vegetables", 60, "Open Field", 5, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 38, "Vegetables", 85, "Greenhouse", 5, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 39, "Vegetables", 50, "Open Field", 5, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 40, "Leafy Greens", 75, "Open Field", 5, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 35, 800m, 5.0m, 25, "Molokhia", "15/3–30/4", "Summer", "Medium" },
                    { 41, "Field Crops", 190, "Open Field", 2, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 42, "Vegetables", 110, "Open Field", 2, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 43, "Vegetables", 60, "Open Field", 2, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 44, "Vegetables", 95, "Open Field", 2, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 45, "Vegetables", 60, "Open Field", 2, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 46, "Vegetables", 85, "Greenhouse", 2, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 47, "Vegetables", 50, "Open Field", 2, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 48, "Vegetables", 55, "Open Field", 2, "1/5–30/6", "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", "70–90", 1500m, 9.0m, 30, 1000m, 5.0m, 20, "Green Beans", "1/3–1/4", "Spring", "Medium" },
                    { 49, "Field Crops", 190, "Open Field", 1, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 50, "Vegetables", 110, "Open Field", 1, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 51, "Vegetables", 60, "Open Field", 1, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 52, "Vegetables", 95, "Open Field", 1, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 53, "Vegetables", 60, "Open Field", 1, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 54, "Vegetables", 85, "Greenhouse", 1, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 55, "Vegetables", 50, "Open Field", 1, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 56, "Vegetables", 100, "Open Field", 1, "1/3–30/4", "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", "40–60", 1200m, 8.0m, 20, 800m, 5.0m, 10, "Peas", "1/11–15/12", "Winter", "Low" },
                    { 57, "Field Crops", 190, "Open Field", 3, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 58, "Field Crops", 180, "Open Field", 3, "1/5–30/6", "https://images.unsplash.com/photo-1533758313437-080353c7a6e1?w=500", "0–10", 800m, 2.0m, 20, 400m, 1.5m, 10, "Barley", "15/11–30/12", "Winter", "Very Low" },
                    { 59, "Vegetables", 110, "Open Field", 3, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 60, "Vegetables", 95, "Open Field", 3, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 61, "Vegetables", 60, "Open Field", 3, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 62, "Vegetables", 50, "Open Field", 3, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 63, "Vegetables", 100, "Open Field", 3, "1/3–30/4", "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", "40–60", 1200m, 8.0m, 20, 800m, 5.0m, 10, "Peas", "1/11–15/12", "Winter", "Low" },
                    { 64, "Field Crops", 160, "Open Field", 3, "1/5–30/6", "https://images.unsplash.com/photo-1515942661900-94b3d197c591?w=500", "0–10", 700m, 6.0m, 20, 400m, 4.0m, 10, "Lentils", "15/11–30/12", "Winter", "Very Low" },
                    { 65, "Field Crops", 190, "Open Field", 4, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 66, "Field Crops", 180, "Open Field", 4, "1/5–30/6", "https://images.unsplash.com/photo-1533758313437-080353c7a6e1?w=500", "0–10", 800m, 2.0m, 20, 400m, 1.5m, 10, "Barley", "15/11–30/12", "Winter", "Very Low" },
                    { 67, "Vegetables", 110, "Open Field", 4, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "70–90", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 68, "Vegetables", 60, "Open Field", 4, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–70", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 69, "Vegetables", 95, "Open Field", 4, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–120", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 70, "Vegetables", 60, "Open Field", 4, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–110", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 71, "Vegetables", 50, "Open Field", 4, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "80–100", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 72, "Vegetables", 100, "Open Field", 4, "1/3–30/4", "https://images.unsplash.com/photo-1587334274328-64186a80aeee?w=500", "40–60", 1200m, 8.0m, 20, 800m, 5.0m, 10, "Peas", "1/11–15/12", "Winter", "Low" },
                    { 73, "Field Crops", 190, "Open Field", 9, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 74, "Vegetables", 110, "Open Field", 9, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 75, "Vegetables", 60, "Open Field", 9, "15/12–31/1", "https://images.unsplash.com/photo-1622206151226-18ca2c9ab4a1?w=500", "50–80", 900m, 7.0m, 20, 600m, 4.0m, 10, "Lettuce", "1/10–20/10", "Winter", "Medium" },
                    { 76, "Vegetables", 95, "Open Field", 9, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–130", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 77, "Vegetables", 60, "Open Field", 9, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 78, "Vegetables", 85, "Greenhouse", 9, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 79, "Vegetables", 50, "Open Field", 9, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 80, "Vegetables", 55, "Open Field", 9, "1/5–30/6", "https://images.unsplash.com/photo-1533116584281-222a104031df?w=500", "70–90", 1500m, 9.0m, 30, 1000m, 5.0m, 20, "Green Beans", "1/3–1/4", "Spring", "Medium" },
                    { 81, "Field Crops", 190, "Open Field", 10, "1/5–30/6", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=500", "0–10", 900m, 2.0m, 20, 500m, 2.0m, 10, "Wheat", "15/11–30/12", "Winter", "Very Low" },
                    { 82, "Vegetables", 110, "Open Field", 10, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Winter", "Medium" },
                    { 83, "Vegetables", 95, "Open Field", 10, "1/6–30/8", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "100–140", 3000m, 4.0m, 30, 2000m, 2.0m, 22, "Tomato", "1/3–15/4", "Summer", "High" },
                    { 84, "Vegetables", 60, "Open Field", 10, "1/5–30/7", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 1800m, 6.0m, 30, 1200m, 3.0m, 22, "Cucumber", "1/3–15/4", "Summer", "High" },
                    { 85, "Vegetables", 85, "Greenhouse", 10, "15/5–30/8", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 3500m, 10.0m, 30, 2500m, 6.0m, 20, "Sweet Pepper", "15/2–15/3", "Summer", "High" },
                    { 86, "Vegetables", 50, "Open Field", 10, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 87, "Vegetables", 110, "Greenhouse", 10, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 30, 2500m, 3.0m, 22, "Eggplant", "1/2–15/3", "Spring", "High" },
                    { 88, "Leafy Greens", 75, "Open Field", 10, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 35, 800m, 5.0m, 25, "Molokhia", "15/3–30/4", "Summer", "Medium" },
                    { 89, "Vegetables", 90, "Greenhouse", 12, "1/12–30/3", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=500", "120–150", 6000m, 5.0m, 25, 5000m, 3.0m, 18, "Tomato", "1/9–15/10", "Winter", "High" },
                    { 90, "Vegetables", 90, "Greenhouse", 12, "1/12–30/3", "https://images.unsplash.com/photo-1604908176997-125f25cc6f3d?w=500", "90–120", 5000m, 5.0m, 25, 3500m, 3.0m, 18, "Cucumber", "1/9–15/10", "Winter", "High" },
                    { 91, "Vegetables", 75, "Greenhouse", 12, "1/12–30/4", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa8a?w=500", "100–130", 4000m, 10.0m, 28, 3000m, 6.0m, 20, "Sweet Pepper", "15/9–15/10", "Winter", "High" },
                    { 92, "Vegetables", 120, "Open Field", 12, "15/2–30/4", "https://images.unsplash.com/photo-1518977676601-b53f82aba655?w=500", "80–100", 1200m, 3.0m, 22, 800m, 2.0m, 15, "Potato", "15/10–30/11", "Spring", "Medium" },
                    { 93, "Vegetables", 100, "Greenhouse", 12, "15/5–30/8", "https://images.unsplash.com/photo-1629856169935-7c9808381ddf?w=500", "100–120", 3500m, 6.0m, 30, 2500m, 3.0m, 22, "Eggplant", "1/2–15/3", "Spring", "High" },
                    { 94, "Vegetables", 60, "Open Field", 12, "15/4–30/6", "https://images.unsplash.com/photo-1590165482129-1b8b27698780?w=500", "90–110", 1800m, 7.0m, 28, 1200m, 4.0m, 18, "Zucchini", "15/2–15/3", "Spring", "High" },
                    { 95, "Fruits", 90, "Open Field", 12, "1/5–15/6", "https://images.unsplash.com/photo-1589984662646-e7b2e4962f18?w=500", "80–100", 2500m, 4.0m, 35, 1500m, 2.0m, 25, "Watermelon", "1/2–1/3", "Summer", "Medium" },
                    { 96, "Leafy Greens", 75, "Open Field", 12, "1/6–30/8", "https://images.unsplash.com/photo-1625944227393-276182bf544e?w=500", "60–80", 1200m, 9.0m, 35, 800m, 5.0m, 25, "Molokhia", "15/3–30/4", "Summer", "Medium" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lands_GovernorateId",
                table: "Lands",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Lands_SoilTypeId",
                table: "Lands",
                column: "SoilTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProfiles_GovernorateId",
                table: "ExpertProfiles",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_GovernorateId",
                table: "Crops",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertProfiles_Governorates_GovernorateId",
                table: "ExpertProfiles",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lands_Governorates_GovernorateId",
                table: "Lands",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lands_SoilTypes_SoilTypeId",
                table: "Lands",
                column: "SoilTypeId",
                principalTable: "SoilTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertProfiles_Governorates_GovernorateId",
                table: "ExpertProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Lands_Governorates_GovernorateId",
                table: "Lands");

            migrationBuilder.DropForeignKey(
                name: "FK_Lands_SoilTypes_SoilTypeId",
                table: "Lands");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "SoilTypes");

            migrationBuilder.DropTable(
                name: "Governorates");

            migrationBuilder.DropIndex(
                name: "IX_Lands_GovernorateId",
                table: "Lands");

            migrationBuilder.DropIndex(
                name: "IX_Lands_SoilTypeId",
                table: "Lands");

            migrationBuilder.DropIndex(
                name: "IX_ExpertProfiles_GovernorateId",
                table: "ExpertProfiles");

            migrationBuilder.RenameColumn(
                name: "SoilTypeId",
                table: "Lands",
                newName: "SoilId");

            migrationBuilder.RenameColumn(
                name: "GovernorateId",
                table: "Lands",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "GovernorateId",
                table: "ExpertProfiles",
                newName: "CityId");

            migrationBuilder.AlterColumn<int>(
                name: "Day",
                table: "ExpertAvailabilities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
