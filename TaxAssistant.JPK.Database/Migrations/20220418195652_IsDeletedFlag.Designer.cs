﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxAssistant.JPK.Database;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220418195652_IsDeletedFlag")]
    partial class IsDeletedFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Import", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("KpirId");

                    b.ToTable("Import");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalStatisticNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("KpirId");

                    b.ToTable("KpirCompany");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompanyAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LocalNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voivodeship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KpirComapnyAddress");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirControlData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RowCount")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalIncome")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("KpirId")
                        .IsUnique();

                    b.ToTable("KpirControlData");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirHeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormVariant")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Purpose")
                        .HasColumnType("int");

                    b.Property<string>("TaxOfficeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KpirId")
                        .IsUnique();

                    b.ToTable("KpirHeader");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirPhysicalInventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("KpirId");

                    b.ToTable("KpirPhisicalInventory");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdditionalField15")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("CostGoods")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CostGoodsRelated")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CostOther")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CostSalaries")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CostTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal?>("ResearchAndDevelopmentCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ResearchAndDevelopmentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("RevenueOther")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueSold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KpirId");

                    b.ToTable("KpirRow");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirSummary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PhysicalInventoryYearEnd")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PhysicalInventoryYearStart")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalIncome")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("KpirId")
                        .IsUnique();

                    b.ToTable("KpirSummary");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Import", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithMany()
                        .HasForeignKey("KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompany", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompanyAddress", "Address")
                        .WithOne("Company")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompany", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithMany()
                        .HasForeignKey("KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirControlData", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithOne("ControlData")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirControlData", "KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirHeader", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithOne("Header")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirHeader", "KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirPhysicalInventory", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithMany("PhysicalInventories")
                        .HasForeignKey("KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirRow", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithMany("Rows")
                        .HasForeignKey("KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirSummary", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithOne("Summary")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirSummary", "KpirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kpir");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", b =>
                {
                    b.Navigation("ControlData")
                        .IsRequired();

                    b.Navigation("Header")
                        .IsRequired();

                    b.Navigation("PhysicalInventories");

                    b.Navigation("Rows");

                    b.Navigation("Summary")
                        .IsRequired();
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompanyAddress", b =>
                {
                    b.Navigation("Company")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
