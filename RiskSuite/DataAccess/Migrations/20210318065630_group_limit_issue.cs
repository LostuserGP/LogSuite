using Microsoft.EntityFrameworkCore.Migrations;

namespace RiskSuite.DataAccess.Migrations
{
    public partial class group_limit_issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "g_limit_bank",
                table: "rating_groups",
                newName: "group_limit_bank");

            migrationBuilder.RenameColumn(
                name: "g_limit",
                table: "rating_groups",
                newName: "group_limit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "group_limit_bank",
                table: "rating_groups",
                newName: "g_limit_bank");

            migrationBuilder.RenameColumn(
                name: "group_limit",
                table: "rating_groups",
                newName: "g_limit");
        }
    }
}
