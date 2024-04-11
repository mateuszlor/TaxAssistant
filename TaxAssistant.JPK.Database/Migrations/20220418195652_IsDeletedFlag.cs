using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    public partial class IsDeletedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirSummary",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirRow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirPhisicalInventory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirHeader",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirControlData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirCompany",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KpirComapnyAddress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Kpir",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Import",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirSummary");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirRow");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirPhisicalInventory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirHeader");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirControlData");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirCompany");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KpirComapnyAddress");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Kpir");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Import");
        }
    }
}
