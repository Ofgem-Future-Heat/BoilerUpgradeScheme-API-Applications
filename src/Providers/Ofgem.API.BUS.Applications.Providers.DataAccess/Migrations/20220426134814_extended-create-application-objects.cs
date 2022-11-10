using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class extendedcreateapplicationobjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_TechTypes_TechTypeID",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyOwnerAddressId",
                table: "PropertyOwnerDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "PropertyOwnerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TechTypeID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuoteAmount",
                table: "Applications",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyOwnerDetailID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApplicationReceived",
                table: "Applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfQuote",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EpcExists",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EpcId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FuelTypeOther",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAssistedDigital",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBeingAudited",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGasGrid",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLoftCavityExempt",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNewBuild",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelfBuild",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWelshTranslation",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousFuelType",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyType",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuoteReference",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuralStatus",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TechnologyCost",
                table: "Applications",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EPCs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpcReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPCs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwnerAddresses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UPRN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwnerAddresses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers",
                column: "ApplicationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnerDetails_PropertyOwnerAddressId",
                table: "PropertyOwnerDetails",
                column: "PropertyOwnerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EpcId",
                table: "Applications",
                column: "EpcId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_EPCs_EpcId",
                table: "Applications",
                column: "EpcId",
                principalTable: "EPCs",
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

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnerDetails_PropertyOwnerAddresses_PropertyOwnerAddressId",
                table: "PropertyOwnerDetails",
                column: "PropertyOwnerAddressId",
                principalTable: "PropertyOwnerAddresses",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_EPCs_EpcId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_TechTypes_TechTypeID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnerDetails_PropertyOwnerAddresses_PropertyOwnerAddressId",
                table: "PropertyOwnerDetails");

            migrationBuilder.DropTable(
                name: "EPCs");

            migrationBuilder.DropTable(
                name: "PropertyOwnerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_PropertyOwnerDetails_PropertyOwnerAddressId",
                table: "PropertyOwnerDetails");

            migrationBuilder.DropIndex(
                name: "IX_Applications_EpcId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PropertyOwnerAddressId",
                table: "PropertyOwnerDetails");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "PropertyOwnerDetails");

            migrationBuilder.DropColumn(
                name: "DateApplicationReceived",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DateOfQuote",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EpcExists",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EpcId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "FuelTypeOther",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsAssistedDigital",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsBeingAudited",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsGasGrid",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsLoftCavityExempt",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsNewBuild",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsSelfBuild",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsWelshTranslation",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PreviousFuelType",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "QuoteReference",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "RuralStatus",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "TechnologyCost",
                table: "Applications");

            migrationBuilder.AlterColumn<Guid>(
                name: "TechTypeID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuoteAmount",
                table: "Applications",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyOwnerDetailID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers",
                column: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailID",
                table: "Applications",
                column: "PropertyOwnerDetailID",
                principalTable: "PropertyOwnerDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_TechTypes_TechTypeID",
                table: "Applications",
                column: "TechTypeID",
                principalTable: "TechTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
