using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLandEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FarmerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    SoilId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    WaterSourceType = table.Column<string>(type: "text", nullable: false),
                    WaterAvailability = table.Column<string>(type: "text", nullable: false),
                    EnvironmentType = table.Column<string>(type: "text", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lands_Users_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lands_FarmerId",
                table: "Lands",
                column: "FarmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lands");
        }
    }
}
