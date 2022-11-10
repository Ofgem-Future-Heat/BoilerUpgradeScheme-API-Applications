using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class statusamendsaddvoucherstatushistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatusHistories_ApplicationSubStatus_ApplicationSubStatusId",
                table: "ApplicationStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSubStatus_ApplicationStatus_ApplicationStatusId",
                table: "ApplicationSubStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusID",
                table: "Vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherSubStatus_VoucherStatus_VoucherStatusId",
                table: "VoucherSubStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherSubStatus",
                table: "VoucherSubStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherStatus",
                table: "VoucherStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSubStatus",
                table: "ApplicationSubStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStatus",
                table: "ApplicationStatus");

            migrationBuilder.RenameTable(
                name: "VoucherSubStatus",
                newName: "VoucherSubStatuses");

            migrationBuilder.RenameTable(
                name: "VoucherStatus",
                newName: "VoucherStatuses");

            migrationBuilder.RenameTable(
                name: "ApplicationSubStatus",
                newName: "ApplicationSubStatuses");

            migrationBuilder.RenameTable(
                name: "ApplicationStatus",
                newName: "ApplicationStatuses");

            migrationBuilder.RenameColumn(
                name: "ChangeTo",
                table: "ApplicationStatusHistories",
                newName: "EndDateTime");

            migrationBuilder.RenameColumn(
                name: "ChangeFrom",
                table: "ApplicationStatusHistories",
                newName: "StartDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherSubStatus_VoucherStatusId",
                table: "VoucherSubStatuses",
                newName: "IX_VoucherSubStatuses_VoucherStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationSubStatus_ApplicationStatusId",
                table: "ApplicationSubStatuses",
                newName: "IX_ApplicationSubStatuses_ApplicationStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "VoucherStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VoucherStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "VoucherStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApplicationSubStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApplicationSubStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ApplicationSubStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApplicationStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApplicationStatuses",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ApplicationStatuses",
                type: "varchar(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherSubStatuses",
                table: "VoucherSubStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherStatuses",
                table: "VoucherStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSubStatuses",
                table: "ApplicationSubStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStatuses",
                table: "ApplicationStatuses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VoucherStatusHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherSubStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherStatusHistories_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherStatusHistories_VoucherSubStatuses_VoucherSubStatusId",
                        column: x => x.VoucherSubStatusId,
                        principalTable: "VoucherSubStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherStatusHistories_VoucherId",
                table: "VoucherStatusHistories",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherStatusHistories_VoucherSubStatusId",
                table: "VoucherStatusHistories",
                column: "VoucherSubStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatuses_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatusHistories_ApplicationSubStatuses_ApplicationSubStatusId",
                table: "ApplicationStatusHistories",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSubStatuses_ApplicationStatuses_ApplicationStatusId",
                table: "ApplicationSubStatuses",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatuses_VoucherSubStatusID",
                table: "Vouchers",
                column: "VoucherSubStatusID",
                principalTable: "VoucherSubStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherSubStatuses_VoucherStatuses_VoucherStatusId",
                table: "VoucherSubStatuses",
                column: "VoucherStatusId",
                principalTable: "VoucherStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationSubStatuses_ApplicationSubStatusId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatusHistories_ApplicationSubStatuses_ApplicationSubStatusId",
                table: "ApplicationStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSubStatuses_ApplicationStatuses_ApplicationStatusId",
                table: "ApplicationSubStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherSubStatuses_VoucherSubStatusID",
                table: "Vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherSubStatuses_VoucherStatuses_VoucherStatusId",
                table: "VoucherSubStatuses");

            migrationBuilder.DropTable(
                name: "VoucherStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherSubStatuses",
                table: "VoucherSubStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherStatuses",
                table: "VoucherStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSubStatuses",
                table: "ApplicationSubStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStatuses",
                table: "ApplicationStatuses");

            migrationBuilder.RenameTable(
                name: "VoucherSubStatuses",
                newName: "VoucherSubStatus");

            migrationBuilder.RenameTable(
                name: "VoucherStatuses",
                newName: "VoucherStatus");

            migrationBuilder.RenameTable(
                name: "ApplicationSubStatuses",
                newName: "ApplicationSubStatus");

            migrationBuilder.RenameTable(
                name: "ApplicationStatuses",
                newName: "ApplicationStatus");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "ApplicationStatusHistories",
                newName: "ChangeFrom");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "ApplicationStatusHistories",
                newName: "ChangeTo");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherSubStatuses_VoucherStatusId",
                table: "VoucherSubStatus",
                newName: "IX_VoucherSubStatus_VoucherStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationSubStatuses_ApplicationStatusId",
                table: "ApplicationSubStatus",
                newName: "IX_ApplicationSubStatus_ApplicationStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "VoucherStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VoucherStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "VoucherStatus",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldUnicode: false,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApplicationSubStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApplicationSubStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ApplicationSubStatus",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldUnicode: false,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "ApplicationStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApplicationStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ApplicationStatus",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldUnicode: false,
                oldMaxLength: 32);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherSubStatus",
                table: "VoucherSubStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherStatus",
                table: "VoucherStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSubStatus",
                table: "ApplicationSubStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStatus",
                table: "ApplicationStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationSubStatus_ApplicationSubStatusId",
                table: "Applications",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatusHistories_ApplicationSubStatus_ApplicationSubStatusId",
                table: "ApplicationStatusHistories",
                column: "ApplicationSubStatusId",
                principalTable: "ApplicationSubStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSubStatus_ApplicationStatus_ApplicationStatusId",
                table: "ApplicationSubStatus",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherSubStatus_VoucherSubStatusID",
                table: "Vouchers",
                column: "VoucherSubStatusID",
                principalTable: "VoucherSubStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherSubStatus_VoucherStatus_VoucherStatusId",
                table: "VoucherSubStatus",
                column: "VoucherStatusId",
                principalTable: "VoucherStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
