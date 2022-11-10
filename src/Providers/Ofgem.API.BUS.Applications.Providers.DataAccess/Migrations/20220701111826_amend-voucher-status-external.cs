using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class amendvoucherstatusexternal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "VoucherStatuses",
                newName: "DisplayLegend");

            migrationBuilder.AddColumn<string>(
                name: "ConsentState",
                table: "VoucherStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentState",
                table: "VoucherStatuses");

            migrationBuilder.DropColumn(
                name: "SearchPattern",
                table: "VoucherStatuses");

            migrationBuilder.RenameColumn(
                name: "DisplayLegend",
                table: "VoucherStatuses",
                newName: "DisplayName");
        }
    }
}
