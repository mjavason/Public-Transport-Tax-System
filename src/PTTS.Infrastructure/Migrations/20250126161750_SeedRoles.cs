using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PTTS.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class SeedRoles : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "6107a220-ed7d-451a-a26a-c8fff0f845eb", null, "SuperAdmin", "SUPERADMIN" },
					{ "c73136f5-9102-4048-b4e4-c6f3756adec8", null, "Admin", "ADMIN" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "6107a220-ed7d-451a-a26a-c8fff0f845eb");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "c73136f5-9102-4048-b4e4-c6f3756adec8");
		}
	}
}
