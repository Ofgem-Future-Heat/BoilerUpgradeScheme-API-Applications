using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class amendapplicationstatusexternal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "ApplicationStatuses",
                newName: "DisplayLegend");

            migrationBuilder.AddColumn<string>(
                name: "ConsentState",
                table: "ApplicationStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentState",
                table: "ApplicationStatuses");

            migrationBuilder.DropColumn(
                name: "SearchPattern",
                table: "ApplicationStatuses");

            migrationBuilder.RenameColumn(
                name: "DisplayLegend",
                table: "ApplicationStatuses",
                newName: "DisplayName");
        }
    }
}
