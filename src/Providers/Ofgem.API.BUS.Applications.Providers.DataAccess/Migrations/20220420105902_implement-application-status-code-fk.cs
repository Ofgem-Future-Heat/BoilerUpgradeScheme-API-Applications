using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class implementapplicationstatuscodefk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationSubStatusId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationSubStatusId",
                table: "Applications");
        }
    }
}
