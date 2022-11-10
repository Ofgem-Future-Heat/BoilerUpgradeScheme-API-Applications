using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class nullepc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_EPCs_EpcId",
                table: "Applications");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpcId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_EPCs_EpcId",
                table: "Applications",
                column: "EpcId",
                principalTable: "EPCs",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_EPCs_EpcId",
                table: "Applications");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpcId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_EPCs_EpcId",
                table: "Applications",
                column: "EpcId",
                principalTable: "EPCs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
