using Microsoft.EntityFrameworkCore.Migrations;

namespace RiskSuite.DataAccess.Migrations
{
    public partial class srk_implement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "causes",
                table: "counterparties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "duns",
                table: "counterparties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_srk",
                table: "counterparties",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "causes",
                table: "counterparties");

            migrationBuilder.DropColumn(
                name: "duns",
                table: "counterparties");

            migrationBuilder.DropColumn(
                name: "is_srk",
                table: "counterparties");
        }
    }
}
