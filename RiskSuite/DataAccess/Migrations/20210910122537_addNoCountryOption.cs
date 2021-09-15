using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class addNoCountryOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "show_gis_phg",
                table: "gises",
                newName: "no_phg");

            migrationBuilder.AddColumn<bool>(
                name: "no_country",
                table: "gises",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "no_country",
                table: "gises");

            migrationBuilder.RenameColumn(
                name: "no_phg",
                table: "gises",
                newName: "show_gis_phg");
        }
    }
}
