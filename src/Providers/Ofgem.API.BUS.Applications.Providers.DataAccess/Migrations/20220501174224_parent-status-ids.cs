using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class parentstatusids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatuses_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_SubStatusId",
                table: "Applications",
                column: "SubStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatuses_SubStatusId",
                table: "Applications",
                column: "SubStatusId",
                principalTable: "ApplicationSubStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatuses_SubStatusId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_SubStatusId",
                table: "Applications");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationSubStatusId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatuses_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatuses",
                principalColumn: "Id");
        }
    }
}
