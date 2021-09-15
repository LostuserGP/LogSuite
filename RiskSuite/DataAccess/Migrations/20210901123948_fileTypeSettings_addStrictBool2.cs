using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class fileTypeSettings_addStrictBool2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "strict_allocated_value_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_country_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_estimated_value_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_fact_value_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_gis_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_must_have",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_not_have",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_requested_value_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strict_allocated_value_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_country_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_estimated_value_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_fact_value_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_gis_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_must_have",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_not_have",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_requested_value_entry",
                table: "file_type_settings");
        }
    }
}
