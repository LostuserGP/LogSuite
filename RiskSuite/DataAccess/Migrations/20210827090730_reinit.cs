using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class reinit : Migration
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
                name: "fk_gis_countries_countries_country_id",
                table: "gis_countries");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_gis_countries_gis_country_id",
                table: "gis_country_values");

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
                name: "fk_gis_phg_values_input_file_logs_allocated_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_phg_values_input_file_logs_estimated_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_phg_values_input_file_logs_fact_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_phg_values_input_file_logs_requested_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropColumn(
                name: "op_gis_country_id",
                table: "gis_country_values");

            migrationBuilder.RenameColumn(
                name: "report_date",
                table: "gis_country_values",
                newName: "date_report");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_phg_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_phg_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_phg_values",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_phg_values",
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
                name: "gis_country_id",
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
                name: "country_id",
                table: "gis_countries",
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
                name: "fk_gis_countries_countries_country_id",
                table: "gis_countries",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_gis_countries_gis_country_id",
                table: "gis_country_values",
                column: "gis_country_id",
                principalTable: "gis_countries",
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
                name: "fk_gis_phg_values_input_file_logs_allocated_value_time_id",
                table: "gis_phg_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_phg_values_input_file_logs_estimated_value_time_id",
                table: "gis_phg_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_phg_values_input_file_logs_fact_value_time_id",
                table: "gis_phg_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_phg_values_input_file_logs_requested_value_time_id",
                table: "gis_phg_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
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
                name: "fk_gis_countries_countries_country_id",
                table: "gis_countries");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_country_values_gis_countries_gis_country_id",
                table: "gis_country_values");

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
                name: "fk_gis_phg_values_input_file_logs_allocated_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_phg_values_input_file_logs_estimated_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_phg_values_input_file_logs_fact_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_phg_values_input_file_logs_requested_value_time_id",
                table: "gis_phg_values");

            migrationBuilder.RenameColumn(
                name: "date_report",
                table: "gis_country_values",
                newName: "report_date");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_phg_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_phg_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_phg_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_phg_values",
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
                name: "gis_country_id",
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

            migrationBuilder.AddColumn<int>(
                name: "op_gis_country_id",
                table: "gis_country_values",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "country_id",
                table: "gis_countries",
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
                name: "fk_gis_countries_countries_country_id",
                table: "gis_countries",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_country_values_gis_countries_gis_country_id",
                table: "gis_country_values",
                column: "gis_country_id",
                principalTable: "gis_countries",
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
                name: "fk_gis_phg_values_input_file_logs_allocated_value_time_id",
                table: "gis_phg_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_phg_values_input_file_logs_estimated_value_time_id",
                table: "gis_phg_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_phg_values_input_file_logs_fact_value_time_id",
                table: "gis_phg_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_phg_values_input_file_logs_requested_value_time_id",
                table: "gis_phg_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
