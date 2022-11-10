﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ofgem.API.BUS.Applications.Providers.DataAccess;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationsDBContext))]
    [Migration("20220517104534_removed-consent-state")]
    partial class removedconsentstate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Application", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BusinessAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DARecommendation")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DateOfQuote")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRejected")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateWithdrawn")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("EpcExemption")
                        .HasColumnType("bit");

                    b.Property<bool?>("EpcExists")
                        .HasColumnType("bit");

                    b.Property<Guid?>("EpcId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FuelTypeOther")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("InstallationAddressID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InstallationDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("InstallerDeclaration")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsBeingAudited")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsLoftCavityExempt")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsNewBuild")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPreviousMeasure")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsSelfBuild")
                        .HasColumnType("bit");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PreviousFuelType")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ProperlyMadeDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("PropertyOwnerConsentIssued")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PropertyOwnerDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PropertyType")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<bool?>("QCRecommendation")
                        .HasColumnType("bit");

                    b.Property<decimal?>("QuoteAmount")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("QuoteProductPrice")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("QuoteReferenceNumber")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ReferenceNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("ReviewRecommendation")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<Guid?>("SubStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SubmitterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TechTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("EpcId");

                    b.HasIndex("InstallationAddressID");

                    b.HasIndex("InstallationDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("PropertyOwnerDetailId");

                    b.HasIndex("SubStatusId");

                    b.HasIndex("TechTypeId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .HasDatabaseName("AS_Index_Code");

                    b.ToTable("ApplicationStatuses");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationSubStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ApplicationSubStatusId");

                    b.ToTable("ApplicationStatusHistories");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationStatusId");

                    b.HasIndex("Code")
                        .HasDatabaseName("ASS_Index_Code");

                    b.ToTable("ApplicationSubStatuses");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ConsentRequest", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ConsentExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ConsentIssuedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ConsentReceivedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationID");

                    b.ToTable("ConsentRequests");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Epc", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EpcReferenceNumber")
                        .HasMaxLength(24)
                        .IsUnicode(false)
                        .HasColumnType("varchar(24)");

                    b.HasKey("ID");

                    b.ToTable("EPCs");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.GlobalSetting", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("GeneratedByID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NextApplicationReferenceNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NextApplicationReferenceNumber"), 10000000L, 1);

                    b.HasKey("ID");

                    b.ToTable("GlobalSettings");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Grant", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("TechTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("TechTypeID");

                    b.ToTable("Grants");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.InstallationAddress", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("County")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool?>("IsGasGrid")
                        .HasColumnType("bit");

                    b.Property<string>("IsRural")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("UPRN")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .IsFixedLength();

                    b.HasKey("ID");

                    b.HasIndex("UPRN")
                        .HasDatabaseName("IA_Index_UPRN");

                    b.ToTable("InstallationAddresses");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.InstallationDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlternativeHeatingFuelDescription")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AlternativeHeatingSystemDescription")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("AlternativeHeatingSystemFuelId")
                        .HasColumnType("int");

                    b.Property<string>("AlternativeHeatingSystemId")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Capacity")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("CommissioningDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FlowTemperature")
                        .HasColumnType("int");

                    b.Property<string>("MCSCertifiedProductName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MCSProductCode")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("MCSProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductManufacturer")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SCOP")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SystemDesignedToProvideDescription")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SystemDesignedToProvideId")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal?>("TotalInstallationCost")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.ToTable("InstallationDetails");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CertifiedFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CertifiedTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("MCSModelNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MCSProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProductTypeDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProductTypeId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SCOP35ToSCOP65")
                        .HasColumnType("int");

                    b.Property<string>("TechnologyDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TechnologyId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.PropertyOwnerAddress", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("County")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("UPRN")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .IsFixedLength();

                    b.HasKey("ID");

                    b.ToTable("PropertyOwnerAddresses");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.PropertyOwnerDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool?>("IsAssistedDigital")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsWelshTranslation")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PropertyOwnerAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TelephoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("ID");

                    b.HasIndex("PropertyOwnerAddressId");

                    b.ToTable("PropertyOwnerDetails");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.TechType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ExpiryIntervalMonths")
                        .HasColumnType("int");

                    b.Property<Guid?>("MCSTechTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TechTypeDescription")
                        .IsRequired()
                        .HasMaxLength(127)
                        .IsUnicode(false)
                        .HasColumnType("varchar(127)");

                    b.HasKey("ID");

                    b.ToTable("TechTypes");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Voucher", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("DARecommendation")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GrantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("QCRecommendation")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RedemptionRequestDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("VoucherSubStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationID")
                        .IsUnique();

                    b.HasIndex("VoucherSubStatusID");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SortOrder")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .HasDatabaseName("VS_Index_Code");

                    b.ToTable("VoucherStatuses");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VoucherSubStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId");

                    b.HasIndex("VoucherSubStatusId");

                    b.ToTable("VoucherStatusHistories");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherSubStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<Guid>("VoucherStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .HasDatabaseName("VSS_Index_Code");

                    b.HasIndex("VoucherStatusId");

                    b.ToTable("VoucherSubStatuses");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Application", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.Epc", "Epc")
                        .WithMany()
                        .HasForeignKey("EpcId");

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.InstallationAddress", "InstallationAddress")
                        .WithMany()
                        .HasForeignKey("InstallationAddressID");

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.InstallationDetail", "InstallationDetail")
                        .WithMany()
                        .HasForeignKey("InstallationDetailId");

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.PropertyOwnerDetail", "PropertyOwnerDetail")
                        .WithMany()
                        .HasForeignKey("PropertyOwnerDetailId");

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus", "SubStatus")
                        .WithMany()
                        .HasForeignKey("SubStatusId");

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.TechType", "TechType")
                        .WithMany()
                        .HasForeignKey("TechTypeId");

                    b.Navigation("Epc");

                    b.Navigation("InstallationAddress");

                    b.Navigation("InstallationDetail");

                    b.Navigation("Product");

                    b.Navigation("PropertyOwnerDetail");

                    b.Navigation("SubStatus");

                    b.Navigation("TechType");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationStatusHistory", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.Application", "Application")
                        .WithMany("ApplicationStatusHistories")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus", "ApplicationSubStatus")
                        .WithMany("ApplicationStatusHistories")
                        .HasForeignKey("ApplicationSubStatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("ApplicationSubStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.ApplicationStatus", "ApplicationStatus")
                        .WithMany("ApplicationSubStatus")
                        .HasForeignKey("ApplicationStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ConsentRequest", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.Application", "Application")
                        .WithMany("ConsentRequests")
                        .HasForeignKey("ApplicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Grant", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.TechType", "TechType")
                        .WithMany()
                        .HasForeignKey("TechTypeID");

                    b.Navigation("TechType");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.PropertyOwnerDetail", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.PropertyOwnerAddress", "PropertyOwnerAddress")
                        .WithMany()
                        .HasForeignKey("PropertyOwnerAddressId");

                    b.Navigation("PropertyOwnerAddress");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Voucher", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.Application", "Application")
                        .WithOne("Voucher")
                        .HasForeignKey("Ofgem.API.BUS.Applications.Domain.Voucher", "ApplicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.VoucherSubStatus", "VoucherSubStatus")
                        .WithMany()
                        .HasForeignKey("VoucherSubStatusID");

                    b.Navigation("Application");

                    b.Navigation("VoucherSubStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherStatusHistory", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.Voucher", "Voucher")
                        .WithMany("VoucherStatusHistories")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ofgem.API.BUS.Applications.Domain.VoucherSubStatus", "VoucherSubStatus")
                        .WithMany("VoucherStatusHistories")
                        .HasForeignKey("VoucherSubStatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Voucher");

                    b.Navigation("VoucherSubStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherSubStatus", b =>
                {
                    b.HasOne("Ofgem.API.BUS.Applications.Domain.VoucherStatus", "VoucherStatus")
                        .WithMany("VoucherSubStatus")
                        .HasForeignKey("VoucherStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VoucherStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Application", b =>
                {
                    b.Navigation("ApplicationStatusHistories");

                    b.Navigation("ConsentRequests");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationStatus", b =>
                {
                    b.Navigation("ApplicationSubStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.ApplicationSubStatus", b =>
                {
                    b.Navigation("ApplicationStatusHistories");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.Voucher", b =>
                {
                    b.Navigation("VoucherStatusHistories");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherStatus", b =>
                {
                    b.Navigation("VoucherSubStatus");
                });

            modelBuilder.Entity("Ofgem.API.BUS.Applications.Domain.VoucherSubStatus", b =>
                {
                    b.Navigation("VoucherStatusHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
