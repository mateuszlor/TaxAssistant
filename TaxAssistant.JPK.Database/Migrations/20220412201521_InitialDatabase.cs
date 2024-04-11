using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kpir",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kpir", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Import",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RawContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Import_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpirControlData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowCount = table.Column<int>(type: "int", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirControlData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpirControlData_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpirHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormVariant = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxOfficeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpirHeader_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpirPhisicalInventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirPhisicalInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpirPhisicalInventory_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpirRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevenueSold = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueOther = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostGoods = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostGoodsRelated = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostSalaries = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostOther = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalField15 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchAndDevelopmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchAndDevelopmentCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpirRow_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KpirSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhysicalInventoryYearStart = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhysicalInventoryYearEnd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpirSummary_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Import_KpirId",
                table: "Import",
                column: "KpirId");

            migrationBuilder.CreateIndex(
                name: "IX_KpirControlData_KpirId",
                table: "KpirControlData",
                column: "KpirId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpirHeader_KpirId",
                table: "KpirHeader",
                column: "KpirId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpirPhisicalInventory_KpirId",
                table: "KpirPhisicalInventory",
                column: "KpirId");

            migrationBuilder.CreateIndex(
                name: "IX_KpirRow_KpirId",
                table: "KpirRow",
                column: "KpirId");

            migrationBuilder.CreateIndex(
                name: "IX_KpirSummary_KpirId",
                table: "KpirSummary",
                column: "KpirId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Import");

            migrationBuilder.DropTable(
                name: "KpirControlData");

            migrationBuilder.DropTable(
                name: "KpirHeader");

            migrationBuilder.DropTable(
                name: "KpirPhisicalInventory");

            migrationBuilder.DropTable(
                name: "KpirRow");

            migrationBuilder.DropTable(
                name: "KpirSummary");

            migrationBuilder.DropTable(
                name: "Kpir");
        }
    }
}
