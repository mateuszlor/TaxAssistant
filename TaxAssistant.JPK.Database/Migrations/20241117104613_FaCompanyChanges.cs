using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class FaCompanyChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormCode",
                table: "FaHeader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TaxIdentificationNumber",
                table: "FaCompany",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TaxIdentificationNumberType",
                table: "FaCompany",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxIdentificationNumber",
                table: "FaCompany");

            migrationBuilder.DropColumn(
                name: "TaxIdentificationNumberType",
                table: "FaCompany");

            migrationBuilder.AlterColumn<string>(
                name: "FormCode",
                table: "FaHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
