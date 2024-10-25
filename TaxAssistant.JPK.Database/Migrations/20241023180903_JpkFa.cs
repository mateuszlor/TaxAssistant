using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class JpkFa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FaId",
                table: "Import",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaCompanyAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voivodeship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaCompanyAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaInvoiceCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaInvoiceCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaControlData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceRowsCount = table.Column<int>(type: "int", nullable: false),
                    InvoicesCount = table.Column<int>(type: "int", nullable: false),
                    TotalIncomeFromRows = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIncomeFromInvoices = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrdersCount = table.Column<int>(type: "int", nullable: false),
                    TotalIncomeFromOrders = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaControlData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaControlData_Fa_FaId",
                        column: x => x.FaId,
                        principalTable: "Fa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormVariant = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxOfficeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenerationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaHeader_Fa_FaId",
                        column: x => x.FaId,
                        principalTable: "Fa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaOrder_Fa_FaId",
                        column: x => x.FaId,
                        principalTable: "Fa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaCompany_FaCompanyAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "FaCompanyAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaCompany_Fa_FaId",
                        column: x => x.FaId,
                        principalTable: "Fa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPriceNetBaseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatBaseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatBaseRateOtherCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPriceNetRate8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatRate8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatRate8OtherCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPriceNetRate5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatRate5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatRate5OtherCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPriceNetReverseCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatReverseCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatReverseChargeOtherCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPriceNetForeignTransaction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVatForeignTransaction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceNetRate0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceNetVatExempted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAccountingScheme = table.Column<bool>(type: "bit", nullable: false),
                    IssuedByBuyer = table.Column<bool>(type: "bit", nullable: false),
                    ReversedCharge = table.Column<bool>(type: "bit", nullable: false),
                    SplitPayment = table.Column<bool>(type: "bit", nullable: false),
                    VatExemption = table.Column<bool>(type: "bit", nullable: false),
                    VatSubjectiveExemptionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatObjectiveExemptionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatObjectiveExemptionOtherReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedByExecutionProcedure = table.Column<bool>(type: "bit", nullable: false),
                    ExecutionProcedureIssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IssuedByTaxRepresentative = table.Column<bool>(type: "bit", nullable: false),
                    TaxRepresentativeIssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IntraCommunitySupplyOfGoodsForNewVechicle = table.Column<bool>(type: "bit", nullable: false),
                    IntraCommunitySupplyOfGoodsForNewVechicleDateOfAprovalToUse = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntraCommunitySupplyOfGoodsForNewVechicleMileage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntraCommunitySupplyOfGoodsForNewVechicleMotohours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedByNextTaxpayer = table.Column<bool>(type: "bit", nullable: false),
                    VatMarginSchemeForTourism = table.Column<bool>(type: "bit", nullable: false),
                    VatMarginScheme = table.Column<bool>(type: "bit", nullable: false),
                    VatMarginSchemeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreditNoteReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditNoteChangedDocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditNoteChangedDocumentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaInvoice_FaInvoiceCompany_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "FaInvoiceCompany",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaInvoice_FaInvoiceCompany_ExecutionProcedureIssuerId",
                        column: x => x.ExecutionProcedureIssuerId,
                        principalTable: "FaInvoiceCompany",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaInvoice_FaInvoiceCompany_SellerId",
                        column: x => x.SellerId,
                        principalTable: "FaInvoiceCompany",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaInvoice_FaInvoiceCompany_TaxRepresentativeIssuerId",
                        column: x => x.TaxRepresentativeIssuerId,
                        principalTable: "FaInvoiceCompany",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaInvoice_Fa_FaId",
                        column: x => x.FaId,
                        principalTable: "Fa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaOrderRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetricUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalVat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatRate = table.Column<int>(type: "int", nullable: true),
                    VatSpecialRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaOrderRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaOrderRow_FaOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "FaOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaInvoiceRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetricUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPriceGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceGross = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VatRate = table.Column<int>(type: "int", nullable: true),
                    VatRateSpecial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaInvoiceRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaInvoiceRow_FaInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "FaInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Import_FaId",
                table: "Import",
                column: "FaId");

            migrationBuilder.CreateIndex(
                name: "IX_FaCompany_AddressId",
                table: "FaCompany",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaCompany_FaId",
                table: "FaCompany",
                column: "FaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaControlData_FaId",
                table: "FaControlData",
                column: "FaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaHeader_FaId",
                table: "FaHeader",
                column: "FaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaInvoice_BuyerId",
                table: "FaInvoice",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_FaInvoice_ExecutionProcedureIssuerId",
                table: "FaInvoice",
                column: "ExecutionProcedureIssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_FaInvoice_FaId",
                table: "FaInvoice",
                column: "FaId");

            migrationBuilder.CreateIndex(
                name: "IX_FaInvoice_SellerId",
                table: "FaInvoice",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_FaInvoice_TaxRepresentativeIssuerId",
                table: "FaInvoice",
                column: "TaxRepresentativeIssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_FaInvoiceRow_InvoiceId",
                table: "FaInvoiceRow",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FaOrder_FaId",
                table: "FaOrder",
                column: "FaId");

            migrationBuilder.CreateIndex(
                name: "IX_FaOrderRow_OrderId",
                table: "FaOrderRow",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Fa_FaId",
                table: "Import",
                column: "FaId",
                principalTable: "Fa",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_Fa_FaId",
                table: "Import");

            migrationBuilder.DropTable(
                name: "FaCompany");

            migrationBuilder.DropTable(
                name: "FaControlData");

            migrationBuilder.DropTable(
                name: "FaHeader");

            migrationBuilder.DropTable(
                name: "FaInvoiceRow");

            migrationBuilder.DropTable(
                name: "FaOrderRow");

            migrationBuilder.DropTable(
                name: "FaCompanyAddress");

            migrationBuilder.DropTable(
                name: "FaInvoice");

            migrationBuilder.DropTable(
                name: "FaOrder");

            migrationBuilder.DropTable(
                name: "FaInvoiceCompany");

            migrationBuilder.DropTable(
                name: "Fa");

            migrationBuilder.DropIndex(
                name: "IX_Import_FaId",
                table: "Import");

            migrationBuilder.DropColumn(
                name: "FaId",
                table: "Import");
        }
    }
}
