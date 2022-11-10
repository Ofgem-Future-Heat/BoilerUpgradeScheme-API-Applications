using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class implementapplicationstatushistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers");

            migrationBuilder.CreateTable(
                name: "ApplicationStatusHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationSubStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationStatusHistories_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationStatusHistories_ApplicationSubStatus_ApplicationSubStatusId",
                        column: x => x.ApplicationSubStatusId,
                        principalTable: "ApplicationSubStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers",
                column: "ApplicationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatusHistories_ApplicationId",
                table: "ApplicationStatusHistories",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatusHistories_ApplicationSubStatusId",
                table: "ApplicationStatusHistories",
                column: "ApplicationSubStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationStatusHistories");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ApplicationID",
                table: "Vouchers",
                column: "ApplicationID");
        }
    }
}
