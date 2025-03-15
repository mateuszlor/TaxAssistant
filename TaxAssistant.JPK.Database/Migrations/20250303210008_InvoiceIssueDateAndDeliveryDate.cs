using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceIssueDateAndDeliveryDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Invoice",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Invoice",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Invoice");
        }
    }
}
