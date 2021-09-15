using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class valueTimeRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_allocated_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_estimated_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_fact_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_requested_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_allocated_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_estimated_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_fact_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_requested_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_allocated_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_estimated_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_fact_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_requested_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_allocated_value_time_id",
                table: "gis_output_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_estimated_value_time_id",
                table: "gis_output_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_fact_value_time_id",
                table: "gis_output_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_requested_value_time_id",
                table: "gis_output_values");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_allocated_value_time_id",
                table: "gis_addon_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_estimated_value_time_id",
                table: "gis_addon_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_fact_value_time_id",
                table: "gis_addon_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_requested_value_time_id",
                table: "gis_addon_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_allocated_value_time_id",
                table: "gis_country_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_estimated_value_time_id",
                table: "gis_country_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_fact_value_time_id",
                table: "gis_country_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_requested_value_time_id",
                table: "gis_country_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_allocated_value_time_id",
                table: "gis_input_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_estimated_value_time_id",
                table: "gis_input_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_fact_value_time_id",
                table: "gis_input_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_requested_value_time_id",
                table: "gis_input_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_allocated_value_time_id",
                table: "gis_output_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_estimated_value_time_id",
                table: "gis_output_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_fact_value_time_id",
                table: "gis_output_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_requested_value_time_id",
                table: "gis_output_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_allocated_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_estimated_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_fact_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_logs_requested_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_allocated_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_estimated_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_fact_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_input_file_logs_requested_value_time_id",
                table: "gis_country_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_allocated_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_estimated_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_fact_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_input_file_logs_requested_value_time_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_allocated_value_time_id",
                table: "gis_output_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_estimated_value_time_id",
                table: "gis_output_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_fact_value_time_id",
                table: "gis_output_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_input_file_logs_requested_value_time_id",
                table: "gis_output_values");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_allocated_value_time_id",
                table: "gis_addon_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_estimated_value_time_id",
                table: "gis_addon_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_fact_value_time_id",
                table: "gis_addon_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_requested_value_time_id",
                table: "gis_addon_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_allocated_value_time_id",
                table: "gis_country_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_estimated_value_time_id",
                table: "gis_country_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_fact_value_time_id",
                table: "gis_country_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_input_file_logs_requested_value_time_id",
                table: "gis_country_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_allocated_value_time_id",
                table: "gis_input_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_estimated_value_time_id",
                table: "gis_input_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_fact_value_time_id",
                table: "gis_input_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_input_file_logs_requested_value_time_id",
                table: "gis_input_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_allocated_value_time_id",
                table: "gis_output_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_estimated_value_time_id",
                table: "gis_output_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_fact_value_time_id",
                table: "gis_output_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_input_file_logs_requested_value_time_id",
                table: "gis_output_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
