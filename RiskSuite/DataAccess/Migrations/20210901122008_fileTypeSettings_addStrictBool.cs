using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class fileTypeSettings_addStrictBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "strict_allocated_value_entries",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_country_name_entries",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_estimated_value_entries",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_fact_value_entries",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_gis_name_entries",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_must_have_names",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_not_have_names",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "strict_requested_value_entries",
                table: "review_file_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strict_allocated_value_entries",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_country_name_entries",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_estimated_value_entries",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_fact_value_entries",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_gis_name_entries",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_must_have_names",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_not_have_names",
                table: "review_file_types");

            migrationBuilder.DropColumn(
                name: "strict_requested_value_entries",
                table: "review_file_types");
        }
    }
}
