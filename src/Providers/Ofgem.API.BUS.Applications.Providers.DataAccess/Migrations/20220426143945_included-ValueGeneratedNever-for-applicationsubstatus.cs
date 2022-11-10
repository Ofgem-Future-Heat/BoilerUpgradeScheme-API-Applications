using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class includedValueGeneratedNeverforapplicationsubstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusID",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationSubStatusID",
                table: "Applications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationSubStatusID",
                table: "Applications",
                column: "ApplicationSubStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusID",
                table: "Applications",
                column: "ApplicationSubStatusID",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
