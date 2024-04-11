using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    public partial class KpirCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KpirComapnyAddress",
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
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirComapnyAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpirCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KpirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalStatisticNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpirCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpirCompany_Kpir_KpirId",
                        column: x => x.KpirId,
                        principalTable: "Kpir",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KpirCompany_KpirComapnyAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "KpirComapnyAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KpirCompany_AddressId",
                table: "KpirCompany",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpirCompany_KpirId",
                table: "KpirCompany",
                column: "KpirId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KpirCompany");

            migrationBuilder.DropTable(
                name: "KpirComapnyAddress");
        }
    }
}
