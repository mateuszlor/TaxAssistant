using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    /// <inheritdoc />
    public partial class NullableImportFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_Ewp_EwpId",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Import_Kpir_KpirId",
                table: "Import");

            migrationBuilder.AlterColumn<Guid>(
                name: "KpirId",
                table: "Import",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EwpId",
                table: "Import",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Ewp_EwpId",
                table: "Import",
                column: "EwpId",
                principalTable: "Ewp",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Kpir_KpirId",
                table: "Import",
                column: "KpirId",
                principalTable: "Kpir",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_Ewp_EwpId",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Import_Kpir_KpirId",
                table: "Import");

            migrationBuilder.AlterColumn<Guid>(
                name: "KpirId",
                table: "Import",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EwpId",
                table: "Import",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Ewp_EwpId",
                table: "Import",
                column: "EwpId",
                principalTable: "Ewp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Kpir_KpirId",
                table: "Import",
                column: "KpirId",
                principalTable: "Kpir",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
