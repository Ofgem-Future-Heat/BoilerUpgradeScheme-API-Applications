using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class ConsentRequestModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsentIssuedDate",
                table: "ConsentRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsentExpiryDate",
                table: "ConsentRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ConsentRequests",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ConsentRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "ConsentRequests",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "ConsentRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE [ConsentRequests] " +
                "SET [ConsentRequests].[CreatedDate] = [ConsentRequests].[ConsentIssuedDate], " +
                "[ConsentRequests].[LastUpdatedDate] = (SELECT MAX(v) FROM (VALUES ([ConsentRequests].[ConsentIssuedDate]), (COALESCE([ConsentRequests].[ConsentReceivedDate], '1/1/1973'))) AS value(v));");

            migrationBuilder.Sql(
                "UPDATE [ConsentRequests] " +
                "SET [ConsentRequests].[CreatedBy] = apps.[CreatedBy], " +
                "[ConsentRequests].[LastUpdatedBy] = apps.[CreatedBy] " +
                "FROM ConsentRequests ci " +
                "INNER JOIN Applications apps " +
                "ON ci.ApplicationId = apps.ID");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ConsentRequests",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ConsentRequests",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ConsentRequests");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ConsentRequests");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "ConsentRequests");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "ConsentRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsentIssuedDate",
                table: "ConsentRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsentExpiryDate",
                table: "ConsentRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
