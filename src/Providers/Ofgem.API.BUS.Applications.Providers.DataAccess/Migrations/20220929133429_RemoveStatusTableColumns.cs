using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class RemoveStatusTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentState",
                table: "VoucherStatuses");

            migrationBuilder.DropColumn(
                name: "DisplayLegend",
                table: "VoucherStatuses");

            migrationBuilder.DropColumn(
                name: "FurtherInformation",
                table: "VoucherStatuses");

            migrationBuilder.DropColumn(
                name: "SearchPattern",
                table: "VoucherStatuses");

            migrationBuilder.DropColumn(
                name: "ConsentState",
                table: "ApplicationStatuses");

            migrationBuilder.DropColumn(
                name: "DisplayLegend",
                table: "ApplicationStatuses");

            migrationBuilder.DropColumn(
                name: "FurtherInformation",
                table: "ApplicationStatuses");

            migrationBuilder.DropColumn(
                name: "SearchPattern",
                table: "ApplicationStatuses");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "VoucherStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ApplicationStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "VoucherStatuses");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ApplicationStatuses");

            migrationBuilder.AddColumn<string>(
                name: "ConsentState",
                table: "VoucherStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayLegend",
                table: "VoucherStatuses",
                type: "varchar(1024)",
                unicode: false,
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FurtherInformation",
                table: "VoucherStatuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchPattern",
                table: "VoucherStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConsentState",
                table: "ApplicationStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayLegend",
                table: "ApplicationStatuses",
                type: "varchar(1024)",
                unicode: false,
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FurtherInformation",
                table: "ApplicationStatuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchPattern",
                table: "ApplicationStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
