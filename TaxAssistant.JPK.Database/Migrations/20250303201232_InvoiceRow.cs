using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetricUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<int>(type: "int", nullable: false),
                    UnitPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPriceGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceRow_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRow_InvoiceId",
                table: "InvoiceRow",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceRow");
        }
    }
}
