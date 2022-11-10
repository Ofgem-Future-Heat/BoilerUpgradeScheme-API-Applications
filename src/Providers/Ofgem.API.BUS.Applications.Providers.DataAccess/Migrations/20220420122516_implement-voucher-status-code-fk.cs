using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class implementvoucherstatuscodefk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VoucherSubStatusId",
                table: "Vouchers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_VoucherSubStatusId",
                table: "Vouchers",
                column: "VoucherSubStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusId",
                table: "Vouchers",
                column: "VoucherSubStatusId",
                principalTable: "VoucherSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_VoucherSubStatusId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "VoucherSubStatusId",
                table: "Vouchers");
        }
    }
}
