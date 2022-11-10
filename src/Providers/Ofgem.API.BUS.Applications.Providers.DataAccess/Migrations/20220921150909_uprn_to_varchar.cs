using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class uprn_to_varchar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UPRN",
                table: "PropertyOwnerAddresses",
                type: "varchar(12)",
                unicode: false,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(12)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UPRN",
                table: "InstallationAddresses",
                type: "varchar(12)",
                unicode: false,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(12)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 12,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UPRN",
                table: "PropertyOwnerAddresses",
                type: "char(12)",
                unicode: false,
                fixedLength: true,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldUnicode: false,
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UPRN",
                table: "InstallationAddresses",
                type: "char(12)",
                unicode: false,
                fixedLength: true,
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldUnicode: false,
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
