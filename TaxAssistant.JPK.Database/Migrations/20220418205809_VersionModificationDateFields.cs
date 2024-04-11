using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    public partial class VersionModificationDateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirSummary",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirSummary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirRow",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirRow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirPhisicalInventory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirPhisicalInventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirHeader",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirControlData",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirControlData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirCompany",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirCompany",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "KpirComapnyAddress",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KpirComapnyAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Kpir",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Kpir",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Import",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Import",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirSummary");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirSummary");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirRow");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirRow");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirPhisicalInventory");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirPhisicalInventory");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirHeader");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirHeader");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirControlData");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirControlData");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirCompany");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirCompany");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "KpirComapnyAddress");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KpirComapnyAddress");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Kpir");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Kpir");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Import");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Import");
        }
    }
}
