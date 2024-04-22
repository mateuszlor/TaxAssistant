using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class JPK_EWP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KpirCompany_KpirId",
                table: "KpirCompany");

            migrationBuilder.AddColumn<Guid>(
                name: "EwpId",
                table: "Import",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EwpCompanyAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voivodeship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EwpCompanyAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EwpControlData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowCount = table.Column<int>(type: "int", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EwpControlData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ewp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowsControlDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FixedAssetsControlDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ewp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ewp_EwpControlData_FixedAssetsControlDataId",
                        column: x => x.FixedAssetsControlDataId,
                        principalTable: "EwpControlData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ewp_EwpControlData_RowsControlDataId",
                        column: x => x.RowsControlDataId,
                        principalTable: "EwpControlData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EwpCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EwpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalStatisticNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EwpCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EwpCompany_EwpCompanyAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "EwpCompanyAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EwpCompany_Ewp_EwpId",
                        column: x => x.EwpId,
                        principalTable: "Ewp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EwpHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EwpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormVariant = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxOfficeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EwpHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EwpHeader_Ewp_EwpId",
                        column: x => x.EwpId,
                        principalTable: "Ewp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EwpRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EwpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevenueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevenueTaxed17Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed15Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed14Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed12Point5Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed12Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed10Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed8Point5Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed5Point5Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTaxed3Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevenueTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EwpRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EwpRow_Ewp_EwpId",
                        column: x => x.EwpId,
                        principalTable: "Ewp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EwpSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EwpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepreciationRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedInitialValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EwpSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EwpSummary_Ewp_EwpId",
                        column: x => x.EwpId,
                        principalTable: "Ewp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KpirCompany_KpirId",
                table: "KpirCompany",
                column: "KpirId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Import_EwpId",
                table: "Import",
                column: "EwpId");

            migrationBuilder.CreateIndex(
                name: "IX_Ewp_FixedAssetsControlDataId",
                table: "Ewp",
                column: "FixedAssetsControlDataId",
                unique: true,
                filter: "[FixedAssetsControlDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ewp_RowsControlDataId",
                table: "Ewp",
                column: "RowsControlDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EwpCompany_AddressId",
                table: "EwpCompany",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EwpCompany_EwpId",
                table: "EwpCompany",
                column: "EwpId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EwpHeader_EwpId",
                table: "EwpHeader",
                column: "EwpId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EwpRow_EwpId",
                table: "EwpRow",
                column: "EwpId");

            migrationBuilder.CreateIndex(
                name: "IX_EwpSummary_EwpId",
                table: "EwpSummary",
                column: "EwpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Ewp_EwpId",
                table: "Import",
                column: "EwpId",
                principalTable: "Ewp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_Ewp_EwpId",
                table: "Import");

            migrationBuilder.DropTable(
                name: "EwpCompany");

            migrationBuilder.DropTable(
                name: "EwpHeader");

            migrationBuilder.DropTable(
                name: "EwpRow");

            migrationBuilder.DropTable(
                name: "EwpSummary");

            migrationBuilder.DropTable(
                name: "EwpCompanyAddress");

            migrationBuilder.DropTable(
                name: "Ewp");

            migrationBuilder.DropTable(
                name: "EwpControlData");

            migrationBuilder.DropIndex(
                name: "IX_KpirCompany_KpirId",
                table: "KpirCompany");

            migrationBuilder.DropIndex(
                name: "IX_Import_EwpId",
                table: "Import");

            migrationBuilder.DropColumn(
                name: "EwpId",
                table: "Import");

            migrationBuilder.CreateIndex(
                name: "IX_KpirCompany_KpirId",
                table: "KpirCompany",
                column: "KpirId");
        }
    }
}
