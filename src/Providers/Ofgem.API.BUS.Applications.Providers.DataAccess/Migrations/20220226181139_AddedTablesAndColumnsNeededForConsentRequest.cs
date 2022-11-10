using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class AddedTablesAndColumnsNeededForConsentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstallationAddressID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyOwnerDetailID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyOwnerDetailsID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TechTypeID",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ConsentRequests",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsentReceived = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsentRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConsentRequests_Applications_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Applications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallationAddresses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    County = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationAddresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwnerDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwnerDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TechTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TechTypeDescription = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTypes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_InstallationAddressID",
                table: "Applications",
                column: "InstallationAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PropertyOwnerDetailID",
                table: "Applications",
                column: "PropertyOwnerDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_TechTypeID",
                table: "Applications",
                column: "TechTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentRequests_ApplicationID",
                table: "ConsentRequests",
                column: "ApplicationID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_InstallationAddresses_InstallationAddressID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_PropertyOwnerDetails_PropertyOwnerDetailID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_TechTypes_TechTypeID",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "ConsentRequests");

            migrationBuilder.DropTable(
                name: "InstallationAddresses");

            migrationBuilder.DropTable(
                name: "PropertyOwnerDetails");

            migrationBuilder.DropTable(
                name: "TechTypes");

            migrationBuilder.DropIndex(
                name: "IX_Applications_InstallationAddressID",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PropertyOwnerDetailID",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_TechTypeID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "InstallationAddressID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PropertyOwnerDetailID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PropertyOwnerDetailsID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "TechTypeID",
                table: "Applications");
        }
    }
}
