using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class decimalRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_output_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_output_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_output_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_output_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_input_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_input_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_input_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_input_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_country_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_country_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_country_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_country_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "value",
                table: "gis_country_resources",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_addon_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_addon_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_addon_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_addon_values",
                type: "numeric(16,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,8)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_output_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_output_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_output_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_output_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_input_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_input_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_input_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_input_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_country_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_country_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_country_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_country_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "value",
                table: "gis_country_resources",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "requsted_value",
                table: "gis_addon_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fact_value",
                table: "gis_addon_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "estimated_value",
                table: "gis_addon_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "allocated_value",
                table: "gis_addon_values",
                type: "numeric(8,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(16,8)");
        }
    }
}
