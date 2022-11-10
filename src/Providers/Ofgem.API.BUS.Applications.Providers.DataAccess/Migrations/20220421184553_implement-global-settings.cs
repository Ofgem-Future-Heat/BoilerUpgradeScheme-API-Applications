using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class implementglobalsettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusId",
                table: "Vouchers");

            migrationBuilder.RenameColumn(
                name: "VoucherSubStatusId",
                table: "Vouchers",
                newName: "VoucherSubStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_Vouchers_VoucherSubStatusId",
                table: "Vouchers",
                newName: "IX_Vouchers_VoucherSubStatusID");

            migrationBuilder.RenameColumn(
                name: "ApplicationSubStatusId",
                table: "Applications",
                newName: "ApplicationSubStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationSubStatusId",
                table: "Applications",
                newName: "IX_Applications_ApplicationSubStatusID");

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NextApplicationReferenceNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10000000, 1"),
                    GeneratedByID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusID",
                table: "Applications",
                column: "ApplicationSubStatusID",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusID",
                table: "Vouchers",
                column: "VoucherSubStatusID",
                principalTable: "VoucherSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusID",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.RenameColumn(
                name: "VoucherSubStatusID",
                table: "Vouchers",
                newName: "VoucherSubStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Vouchers_VoucherSubStatusID",
                table: "Vouchers",
                newName: "IX_Vouchers_VoucherSubStatusId");

            migrationBuilder.RenameColumn(
                name: "ApplicationSubStatusID",
                table: "Applications",
                newName: "ApplicationSubStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationSubStatusID",
                table: "Applications",
                newName: "IX_Applications_ApplicationSubStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusId",
                table: "Vouchers",
                column: "VoucherSubStatusId",
                principalTable: "VoucherSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
