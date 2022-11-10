using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class amendapplicationstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_InstallationAddresses_InstallationAddressID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_TechTypes_TechTypeID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ConsentRecommendation",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "TechTypeID",
                table: "Applications",
                newName: "TechTypeId");

            migrationBuilder.RenameColumn(
                name: "PropertyOwnerDetailID",
                table: "Applications",
                newName: "PropertyOwnerDetailId");

            migrationBuilder.RenameColumn(
                name: "BusinessAccountID",
                table: "Applications",
                newName: "BusinessAccountId");

            migrationBuilder.RenameColumn(
                name: "ApplicationSubStatusID",
                table: "Applications",
                newName: "ApplicationSubStatusId");

            migrationBuilder.RenameColumn(
                name: "TechnologyCost",
                table: "Applications",
                newName: "QuoteProductPrice");

            migrationBuilder.RenameColumn(
                name: "QuoteReference",
                table: "Applications",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "InstallerUserAccountId",
                table: "Applications",
                newName: "SubmitterId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_TechTypeID",
                table: "Applications",
                newName: "IX_Applications_TechTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_PropertyOwnerDetailID",
                table: "Applications",
                newName: "IX_Applications_PropertyOwnerDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationSubStatusID",
                table: "Applications",
                newName: "IX_Applications_ApplicationSubStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewRecommendation",
                table: "Applications",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Applications",
                type: "varchar(11)",
                unicode: false,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyType",
                table: "Applications",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreviousFuelType",
                table: "Applications",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallationAddressID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "FuelTypeOther",
                table: "Applications",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationSubStatusId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDate",
                table: "Applications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ConsentState",
                table: "Applications",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DARecommendation",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateWithdrawn",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EpcExemption",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InstallerDeclaration",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreviousMeasure",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProperlyMadeDate",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PropertyOwnerConsentIssued",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "QCRecommendation",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteReferenceNumber",
                table: "Applications",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubStatusId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_InstallationAddresses_InstallationAddressID",
                table: "Applications",
                column: "InstallationAddressID",
                principalTable: "InstallationAddresses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailId",
                table: "Applications",
                column: "PropertyOwnerDetailId",
                principalTable: "PropertyOwnerDetails",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_TechTypes_TechTypeId",
                table: "Applications",
                column: "TechTypeId",
                principalTable: "TechTypes",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_InstallationAddresses_InstallationAddressID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_TechTypes_TechTypeId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ConsentState",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DARecommendation",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DateRejected",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DateWithdrawn",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EpcExemption",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "InstallerDeclaration",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsPreviousMeasure",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ProperlyMadeDate",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PropertyOwnerConsentIssued",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "QCRecommendation",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "QuoteReferenceNumber",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "SubStatusId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "TechTypeId",
                table: "Applications",
                newName: "TechTypeID");

            migrationBuilder.RenameColumn(
                name: "PropertyOwnerDetailId",
                table: "Applications",
                newName: "PropertyOwnerDetailID");

            migrationBuilder.RenameColumn(
                name: "BusinessAccountId",
                table: "Applications",
                newName: "BusinessAccountID");

            migrationBuilder.RenameColumn(
                name: "ApplicationSubStatusId",
                table: "Applications",
                newName: "ApplicationSubStatusID");

            migrationBuilder.RenameColumn(
                name: "SubmitterId",
                table: "Applications",
                newName: "InstallerUserAccountId");

            migrationBuilder.RenameColumn(
                name: "QuoteProductPrice",
                table: "Applications",
                newName: "TechnologyCost");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Applications",
                newName: "QuoteReference");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_TechTypeId",
                table: "Applications",
                newName: "IX_Applications_TechTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_PropertyOwnerDetailId",
                table: "Applications",
                newName: "IX_Applications_PropertyOwnerDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationSubStatusId",
                table: "Applications",
                newName: "IX_Applications_ApplicationSubStatusID");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewRecommendation",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "Applications",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldUnicode: false,
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyType",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreviousFuelType",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallationAddressID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FuelTypeOther",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationSubStatusID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDate",
                table: "Applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsentRecommendation",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusID",
                table: "Applications",
                column: "ApplicationSubStatusID",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_InstallationAddresses_InstallationAddressID",
                table: "Applications",
                column: "InstallationAddressID",
                principalTable: "InstallationAddresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailID",
                table: "Applications",
                column: "PropertyOwnerDetailID",
                principalTable: "PropertyOwnerDetails",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_TechTypes_TechTypeID",
                table: "Applications",
                column: "TechTypeID",
                principalTable: "TechTypes",
                principalColumn: "ID");
        }
    }
}
