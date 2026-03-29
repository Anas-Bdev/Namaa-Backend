using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIrrigationMethodToLand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IrrigationMethod",
                table: "Lands",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrrigationMethod",
                table: "Lands");
        }
    }
}
