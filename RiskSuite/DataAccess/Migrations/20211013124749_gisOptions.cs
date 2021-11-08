using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class gisOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "no_country",
                table: "gises",
                newName: "show_on_top");

            migrationBuilder.AddColumn<bool>(
                name: "is_not_calculated",
                table: "gises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_one_row",
                table: "gises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "show_on_bottom",
                table: "gises",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_not_calculated",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "is_one_row",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "show_on_bottom",
                table: "gises");

            migrationBuilder.RenameColumn(
                name: "show_on_top",
                table: "gises",
                newName: "no_country");
        }
    }
}
