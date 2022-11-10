using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofgem.API.BUS.Applications.Providers.DataAccess.Migrations
{
    public partial class ApplicationRefNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "A_Index_ReferenceNumber",
                table: "Applications",
                column: "ReferenceNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "A_Index_ReferenceNumber",
                table: "Applications");
        }
    }
}
