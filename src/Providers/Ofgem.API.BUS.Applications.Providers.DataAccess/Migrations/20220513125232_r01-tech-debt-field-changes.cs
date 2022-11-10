using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class r01techdebtfieldchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatuses_VoucherSubStatusID",
                table: "Vouchers");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherSubStatusID",
                table: "Vouchers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "ExpiryIntervalMonths",
                table: "TechTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatuses_VoucherSubStatusID",
                table: "Vouchers",
                column: "VoucherSubStatusID",
                principalTable: "VoucherSubStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatuses_VoucherSubStatusID",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ExpiryIntervalMonths",
                table: "TechTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherSubStatusID",
                table: "Vouchers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatuses_VoucherSubStatusID",
                table: "Vouchers",
                column: "VoucherSubStatusID",
                principalTable: "VoucherSubStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
