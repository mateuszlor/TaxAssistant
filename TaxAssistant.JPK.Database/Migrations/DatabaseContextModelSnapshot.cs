﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxAssistant.JPK.Database;

#nullable disable

namespace TaxAssistant.JPK.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FixedAssetsControlDataId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RowsControlDataId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FixedAssetsControlDataId")
                        .IsUnique()
                        .HasFilter("[FixedAssetsControlDataId] IS NOT NULL");

                    b.HasIndex("RowsControlDataId")
                        .IsUnique();

                    b.ToTable("Ewp");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EwpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("EwpId")
                        .IsUnique();

                    b.ToTable("EwpCompany");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompanyAddress", b =>
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

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LocalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("Voivodeship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EwpCompanyAddress");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpControlData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowCount")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EwpControlData");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpFixedAsset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AcceptanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CategoryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("DepreciationRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EwpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("InitialValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransferDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("UpdatedInitialValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EwpId");

                    b.ToTable("EwpSummary");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpHeader", b =>
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

                    b.Property<Guid>("EwpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FormCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormVariant")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Purpose")
                        .HasColumnType("int");

                    b.Property<string>("TaxOfficeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EwpId")
                        .IsUnique();

                    b.ToTable("EwpHeader");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EwpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("RevenueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("RevenueTaxed10Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed12Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed12Point5Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed14Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed15Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed17Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed3Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed5Point5Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTaxed8Point5Percent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RevenueTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EwpId");

                    b.ToTable("EwpRow");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Import", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EwpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("KpirId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EwpId");

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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalStatisticNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("KpirId")
                        .IsUnique();

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowCount")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Purpose")
                        .HasColumnType("int");

                    b.Property<string>("TaxOfficeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

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

                    b.Property<int>("Version")
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

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PhysicalInventoryYearEnd")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PhysicalInventoryYearStart")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KpirId")
                        .IsUnique();

                    b.ToTable("KpirSummary");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpControlData", "FixedAssetsControlData")
                        .WithOne()
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "FixedAssetsControlDataId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpControlData", "RowsControlData")
                        .WithOne()
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "RowsControlDataId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FixedAssetsControlData");

                    b.Navigation("RowsControlData");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompany", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompanyAddress", "Address")
                        .WithOne("Company")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompany", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "Ewp")
                        .WithOne("Subject")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompany", "EwpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Ewp");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpFixedAsset", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "Ewp")
                        .WithMany("FixedAssets")
                        .HasForeignKey("EwpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ewp");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpHeader", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "Ewp")
                        .WithOne("Header")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpHeader", "EwpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ewp");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpRow", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "Ewp")
                        .WithMany("Rows")
                        .HasForeignKey("EwpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ewp");
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Import", b =>
                {
                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", "Ewp")
                        .WithMany()
                        .HasForeignKey("EwpId");

                    b.HasOne("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", "Kpir")
                        .WithMany()
                        .HasForeignKey("KpirId");

                    b.Navigation("Ewp");

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
                        .WithOne("Subject")
                        .HasForeignKey("TaxAssistant.JPK.Shared.Model.Database.Kpir.KpirCompany", "KpirId")
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

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.Ewp", b =>
                {
                    b.Navigation("FixedAssets");

                    b.Navigation("Header")
                        .IsRequired();

                    b.Navigation("Rows");

                    b.Navigation("Subject")
                        .IsRequired();
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Ewp.EwpCompanyAddress", b =>
                {
                    b.Navigation("Company")
                        .IsRequired();
                });

            modelBuilder.Entity("TaxAssistant.JPK.Shared.Model.Database.Kpir.Kpir", b =>
                {
                    b.Navigation("ControlData")
                        .IsRequired();

                    b.Navigation("Header")
                        .IsRequired();

                    b.Navigation("PhysicalInventories");

                    b.Navigation("Rows");

                    b.Navigation("Subject")
                        .IsRequired();

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
