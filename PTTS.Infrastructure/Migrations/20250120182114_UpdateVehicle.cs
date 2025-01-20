using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTTS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicTransportVehicles_AspNetUsers_UserId",
                schema: "identity",
                table: "PublicTransportVehicles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "PublicTransportVehicles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                schema: "identity",
                table: "PublicTransportVehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                schema: "identity",
                table: "PublicTransportVehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlateNumber",
                schema: "identity",
                table: "PublicTransportVehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicTransportVehicles_AspNetUsers_UserId",
                schema: "identity",
                table: "PublicTransportVehicles",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicTransportVehicles_AspNetUsers_UserId",
                schema: "identity",
                table: "PublicTransportVehicles");

            migrationBuilder.DropColumn(
                name: "Make",
                schema: "identity",
                table: "PublicTransportVehicles");

            migrationBuilder.DropColumn(
                name: "Model",
                schema: "identity",
                table: "PublicTransportVehicles");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                schema: "identity",
                table: "PublicTransportVehicles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "PublicTransportVehicles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicTransportVehicles_AspNetUsers_UserId",
                schema: "identity",
                table: "PublicTransportVehicles",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
