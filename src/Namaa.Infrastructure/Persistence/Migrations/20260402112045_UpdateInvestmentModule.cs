using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Namaa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvestmentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProfitShare",
                table: "InvestorContributions",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SharePercentage",
                table: "InvestorContributions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualCost",
                table: "InvestmentProjects",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualProfit",
                table: "InvestmentProjects",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualRevenue",
                table: "InvestmentProjects",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitShare",
                table: "InvestorContributions");

            migrationBuilder.DropColumn(
                name: "SharePercentage",
                table: "InvestorContributions");

            migrationBuilder.DropColumn(
                name: "ActualCost",
                table: "InvestmentProjects");

            migrationBuilder.DropColumn(
                name: "ActualProfit",
                table: "InvestmentProjects");

            migrationBuilder.DropColumn(
                name: "ActualRevenue",
                table: "InvestmentProjects");
        }
    }
}
