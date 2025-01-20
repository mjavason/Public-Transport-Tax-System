using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTTS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaxRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalGovernment",
                schema: "identity",
                table: "TaxRates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "identity",
                table: "TaxRates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalGovernment",
                schema: "identity",
                table: "TaxRates");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "identity",
                table: "TaxRates");
        }
    }
}
