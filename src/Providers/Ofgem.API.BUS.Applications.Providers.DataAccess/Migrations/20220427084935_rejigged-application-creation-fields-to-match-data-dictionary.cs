using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class rejiggedapplicationcreationfieldstomatchdatadictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssistedDigital",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsWelshTranslation",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "DateApplicationReceived",
                table: "Applications",
                newName: "ApplicationDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsAssistedDigital",
                table: "PropertyOwnerDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWelshTranslation",
                table: "PropertyOwnerDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "PropertyOwnerAddresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "PropertyOwnerAddresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "PropertyOwnerAddresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssistedDigital",
                table: "PropertyOwnerDetails");

            migrationBuilder.DropColumn(
                name: "IsWelshTranslation",
                table: "PropertyOwnerDetails");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "PropertyOwnerAddresses");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "PropertyOwnerAddresses");

            migrationBuilder.DropColumn(
                name: "County",
                table: "PropertyOwnerAddresses");

            migrationBuilder.RenameColumn(
                name: "ApplicationDate",
                table: "Applications",
                newName: "DateApplicationReceived");

            migrationBuilder.AddColumn<bool>(
                name: "IsAssistedDigital",
                table: "Applications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWelshTranslation",
                table: "Applications",
                type: "bit",
                nullable: true);
        }
    }
}
