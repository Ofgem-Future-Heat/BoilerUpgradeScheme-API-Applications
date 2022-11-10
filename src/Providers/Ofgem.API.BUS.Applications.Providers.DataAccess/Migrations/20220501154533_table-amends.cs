using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class tableamends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grants_TechTypes_TechTypeID",
                table: "Grants");

            migrationBuilder.RenameColumn(
                name: "GrantID",
                table: "Vouchers",
                newName: "GrantId");

            migrationBuilder.RenameColumn(
                name: "RedemptionDate",
                table: "Vouchers",
                newName: "RedemptionRequestDate");

            migrationBuilder.RenameColumn(
                name: "Sent",
                table: "ConsentRequests",
                newName: "ConsentIssuedDate");

            migrationBuilder.RenameColumn(
                name: "Expires",
                table: "ConsentRequests",
                newName: "ConsentExpiryDate");

            migrationBuilder.RenameColumn(
                name: "ConsentReceived",
                table: "ConsentRequests",
                newName: "ConsentReceivedDate");

            migrationBuilder.AddColumn<bool>(
                name: "DARecommendation",
                table: "Vouchers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "QCRecommendation",
                table: "Vouchers",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TechTypeDescription",
                table: "TechTypes",
                type: "varchar(127)",
                unicode: false,
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AddColumn<Guid>(
                name: "MCSTechTypeId",
                table: "TechTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TechTypeID",
                table: "Grants",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Grants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Grants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EpcReferenceNumber",
                table: "EPCs",
                type: "varchar(24)",
                unicode: false,
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Grants_TechTypes_TechTypeID",
                table: "Grants",
                column: "TechTypeID",
                principalTable: "TechTypes",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grants_TechTypes_TechTypeID",
                table: "Grants");

            migrationBuilder.DropColumn(
                name: "DARecommendation",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "QCRecommendation",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "MCSTechTypeId",
                table: "TechTypes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Grants");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Grants");

            migrationBuilder.RenameColumn(
                name: "GrantId",
                table: "Vouchers",
                newName: "GrantID");

            migrationBuilder.RenameColumn(
                name: "RedemptionRequestDate",
                table: "Vouchers",
                newName: "RedemptionDate");

            migrationBuilder.RenameColumn(
                name: "ConsentReceivedDate",
                table: "ConsentRequests",
                newName: "ConsentReceived");

            migrationBuilder.RenameColumn(
                name: "ConsentIssuedDate",
                table: "ConsentRequests",
                newName: "Sent");

            migrationBuilder.RenameColumn(
                name: "ConsentExpiryDate",
                table: "ConsentRequests",
                newName: "Expires");

            migrationBuilder.AlterColumn<string>(
                name: "TechTypeDescription",
                table: "TechTypes",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(127)",
                oldUnicode: false,
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<Guid>(
                name: "TechTypeID",
                table: "Grants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EpcReferenceNumber",
                table: "EPCs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(24)",
                oldUnicode: false,
                oldMaxLength: 24);

            migrationBuilder.AddForeignKey(
                name: "FK_Grants_TechTypes_TechTypeID",
                table: "Grants",
                column: "TechTypeID",
                principalTable: "TechTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
