using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddressVoivodeship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Voivodeship",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Voivodeship",
                table: "Address");
        }
    }
}
