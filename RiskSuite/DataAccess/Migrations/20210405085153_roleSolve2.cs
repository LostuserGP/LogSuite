using Microsoft.EntityFrameworkCore.Migrations;

namespace RiskSuite.DataAccess.Migrations
{
    public partial class roleSolve2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "custom_claim",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "custom_claim",
                table: "AspNetUsers");
        }
    }
}
