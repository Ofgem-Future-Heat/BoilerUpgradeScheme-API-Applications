using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class UpdateAuditLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Applications_ApplicationId",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "AuditLogs",
                newName: "EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_ApplicationId",
                table: "AuditLogs",
                newName: "IX_AuditLogs_EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Applications_EntityId",
                table: "AuditLogs",
                column: "EntityId",
                principalTable: "Applications",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Applications_EntityId",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "AuditLogs",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_EntityId",
                table: "AuditLogs",
                newName: "IX_AuditLogs_ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Applications_ApplicationId",
                table: "AuditLogs",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ID");
        }
    }
}
