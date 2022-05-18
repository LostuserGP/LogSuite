using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogSuite.DataAccess.Migrations
{
    public partial class reinit2 : Migration
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

            migrationBuilder.DropTable(
                name: "country_names");

            migrationBuilder.DropTable(
                name: "gis_addon_names");

            migrationBuilder.DropTable(
                name: "gis_input_names");

            migrationBuilder.DropTable(
                name: "gis_names");

            migrationBuilder.DropTable(
                name: "gis_output_names");

            migrationBuilder.DropIndex(
                name: "ix_gis_output_values_gis_id",
                table: "gis_output_values");

            migrationBuilder.DropIndex(
                name: "ix_gis_input_values_gis_id",
                table: "gis_input_values");

            migrationBuilder.DropIndex(
                name: "ix_gis_country_values_gis_country_id",
                table: "gis_country_values");

            migrationBuilder.DropIndex(
                name: "ix_gis_country_resources_gis_country_id",
                table: "gis_country_resources");

            migrationBuilder.DropIndex(
                name: "ix_gis_countries_gis_id",
                table: "gis_countries");

            migrationBuilder.DropIndex(
                name: "ix_gis_addon_values_gis_addon_id",
                table: "gis_addon_values");

            migrationBuilder.DropColumn(
                name: "date_file",
                table: "input_file_logs");

            migrationBuilder.DropColumn(
                name: "time_file",
                table: "input_file_logs");

            migrationBuilder.DropColumn(
                name: "time_input",
                table: "input_file_logs");

            migrationBuilder.DropColumn(
                name: "date_report",
                table: "gis_output_values");

            migrationBuilder.DropColumn(
                name: "date_report",
                table: "gis_input_values");

            migrationBuilder.DropColumn(
                name: "date_report",
                table: "gis_country_values");

            migrationBuilder.DropColumn(
                name: "multiplicator",
                table: "gis_countries");

            migrationBuilder.DropColumn(
                name: "multiplicator",
                table: "gis_addons");

            migrationBuilder.DropColumn(
                name: "date_report",
                table: "gis_addon_values");

            migrationBuilder.DropColumn(
                name: "ticker",
                table: "countries");

            migrationBuilder.RenameColumn(
                name: "show_on_top",
                table: "gises",
                newName: "is_top");

            migrationBuilder.RenameColumn(
                name: "show_on_bottom",
                table: "gises",
                newName: "is_no_phg");

            migrationBuilder.RenameColumn(
                name: "no_phg",
                table: "gises",
                newName: "is_bottom");

            migrationBuilder.RenameColumn(
                name: "requsted_value",
                table: "gis_output_values",
                newName: "requested_value");

            migrationBuilder.RenameColumn(
                name: "requsted_value",
                table: "gis_input_values",
                newName: "requested_value");

            migrationBuilder.RenameColumn(
                name: "requsted_value",
                table: "gis_country_values",
                newName: "requested_value");

            migrationBuilder.RenameColumn(
                name: "is_calculated",
                table: "gis_countries",
                newName: "is_not_calculated");

            migrationBuilder.RenameColumn(
                name: "is_calculated",
                table: "gis_addons",
                newName: "is_output");

            migrationBuilder.RenameColumn(
                name: "requsted_value",
                table: "gis_addon_values",
                newName: "requested_value");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "rating_internals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "rating_externals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "rating_countries",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "input_file_logs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateOnly>(
                name: "file_date",
                table: "input_file_logs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "file_time",
                table: "input_file_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "input_time",
                table: "input_file_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "guarantees",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_report",
                table: "guarantee_reports",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_expiration",
                table: "guarantee_reports",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_end",
                table: "guarantee_limits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_agree_start",
                table: "guarantee_limits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_agree_end",
                table: "guarantee_limits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_approval",
                table: "guarantee_approval_docs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<List<string>>(
                name: "gis_input_names",
                table: "gises",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "gis_output_names",
                table: "gises",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "names",
                table: "gises",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "requested_value_time_id",
                table: "gis_output_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "fact_value_time_id",
                table: "gis_output_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "estimated_value_time_id",
                table: "gis_output_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "allocated_value_time_id",
                table: "gis_output_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "gis_output_values",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateOnly>(
                name: "report_date",
                table: "gis_output_values",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<long>(
                name: "requested_value_time_id",
                table: "gis_input_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "fact_value_time_id",
                table: "gis_input_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "estimated_value_time_id",
                table: "gis_input_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "allocated_value_time_id",
                table: "gis_input_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "gis_input_values",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateOnly>(
                name: "report_date",
                table: "gis_input_values",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<long>(
                name: "requested_value_time_id",
                table: "gis_country_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "fact_value_time_id",
                table: "gis_country_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "estimated_value_time_id",
                table: "gis_country_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "allocated_value_time_id",
                table: "gis_country_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "gis_country_values",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateOnly>(
                name: "report_date",
                table: "gis_country_values",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "month",
                table: "gis_country_resources",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<bool>(
                name: "is_input",
                table: "gis_addons",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "names",
                table: "gis_addons",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "requested_value_time_id",
                table: "gis_addon_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "fact_value_time_id",
                table: "gis_addon_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "estimated_value_time_id",
                table: "gis_addon_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "allocated_value_time_id",
                table: "gis_addon_values",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "gis_addon_values",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateOnly>(
                name: "report_date",
                table: "gis_addon_values",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "financial_statements",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "type_name",
                table: "file_type_settings",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "currency_rates",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<List<string>>(
                name: "names",
                table: "countries",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "counterparties",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "committees",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateTable(
                name: "department_dto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_department_dto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gis_country_addons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_country_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    daily_review_name = table.Column<string>(type: "text", nullable: true),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    names = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_country_addons", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_country_addons_gis_countries_gis_country_id",
                        column: x => x.gis_country_id,
                        principalTable: "gis_countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_dto",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    department_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_dto", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_dto_department_dto_department_id",
                        column: x => x.department_id,
                        principalTable: "department_dto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "gis_country_addon_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_country_addon_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_comm_gas = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_country_addon_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_country_addon_types_gis_country_addons_gis_country_addo",
                        column: x => x.gis_country_addon_id,
                        principalTable: "gis_country_addons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_country_addon_values",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_country_addon_id = table.Column<int>(type: "integer", nullable: false),
                    report_date = table.Column<DateOnly>(type: "date", nullable: false),
                    requested_value = table.Column<decimal>(type: "numeric(16,8)", nullable: false),
                    requested_value_time_id = table.Column<long>(type: "bigint", nullable: true),
                    allocated_value = table.Column<decimal>(type: "numeric(16,8)", nullable: false),
                    allocated_value_time_id = table.Column<long>(type: "bigint", nullable: true),
                    estimated_value = table.Column<decimal>(type: "numeric(16,8)", nullable: false),
                    estimated_value_time_id = table.Column<long>(type: "bigint", nullable: true),
                    fact_value = table.Column<decimal>(type: "numeric(16,8)", nullable: false),
                    fact_value_time_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_country_addon_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_country_addon_values_gis_country_addons_gis_country_add",
                        column: x => x.gis_country_addon_id,
                        principalTable: "gis_country_addons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_country_addon_values_input_file_logs_allocated_value_ti",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_country_addon_values_input_file_logs_estimated_value_ti",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_country_addon_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_country_addon_values_input_file_logs_requested_value_ti",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "input_file_log_dto",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    filename = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    input_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    file_date = table.Column<DateOnly>(type: "date", nullable: false),
                    file_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_input_file_log_dto", x => x.id);
                    table.ForeignKey(
                        name: "fk_input_file_log_dto_user_dto_user_id",
                        column: x => x.user_id,
                        principalTable: "user_dto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_gis_id_report_date",
                table: "gis_output_values",
                columns: new[] { "gis_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_gis_id_report_date",
                table: "gis_input_values",
                columns: new[] { "gis_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_gis_country_id_report_date",
                table: "gis_country_values",
                columns: new[] { "gis_country_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_resources_gis_country_id_month",
                table: "gis_country_resources",
                columns: new[] { "gis_country_id", "month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_countries_gis_id_country_id",
                table: "gis_countries",
                columns: new[] { "gis_id", "country_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_gis_addon_id_report_date",
                table: "gis_addon_values",
                columns: new[] { "gis_addon_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_file_type_settings_name",
                table: "file_type_settings",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addon_types_gis_country_addon_id",
                table: "gis_country_addon_types",
                column: "gis_country_addon_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addon_values_allocated_value_time_id",
                table: "gis_country_addon_values",
                column: "allocated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addon_values_estimated_value_time_id",
                table: "gis_country_addon_values",
                column: "estimated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addon_values_fact_value_time_id",
                table: "gis_country_addon_values",
                column: "fact_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addon_values_gis_country_addon_id",
                table: "gis_country_addon_values",
                column: "gis_country_addon_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addon_values_requested_value_time_id",
                table: "gis_country_addon_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_addons_gis_country_id",
                table: "gis_country_addons",
                column: "gis_country_id");

            migrationBuilder.CreateIndex(
                name: "ix_input_file_log_dto_user_id",
                table: "input_file_log_dto",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_dto_department_id",
                table: "user_dto",
                column: "department_id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_allocated_value_time_id",
                table: "gis_addon_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_log_dto",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_estimated_value_time_id",
                table: "gis_addon_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_log_dto",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_fact_value_time_id",
                table: "gis_addon_values",
                column: "fact_value_time_id",
                principalTable: "input_file_log_dto",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_requested_value_time_id",
                table: "gis_addon_values",
                column: "requested_value_time_id",
                principalTable: "input_file_log_dto",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_allocated_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_estimated_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_fact_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_addon_values_input_file_log_dto_requested_value_time_id",
                table: "gis_addon_values");

            migrationBuilder.DropTable(
                name: "gis_country_addon_types");

            migrationBuilder.DropTable(
                name: "gis_country_addon_values");

            migrationBuilder.DropTable(
                name: "input_file_log_dto");

            migrationBuilder.DropTable(
                name: "gis_country_addons");

            migrationBuilder.DropTable(
                name: "user_dto");

            migrationBuilder.DropTable(
                name: "department_dto");

            migrationBuilder.DropIndex(
                name: "ix_gis_output_values_gis_id_report_date",
                table: "gis_output_values");

            migrationBuilder.DropIndex(
                name: "ix_gis_input_values_gis_id_report_date",
                table: "gis_input_values");

            migrationBuilder.DropIndex(
                name: "ix_gis_country_values_gis_country_id_report_date",
                table: "gis_country_values");

            migrationBuilder.DropIndex(
                name: "ix_gis_country_resources_gis_country_id_month",
                table: "gis_country_resources");

            migrationBuilder.DropIndex(
                name: "ix_gis_countries_gis_id_country_id",
                table: "gis_countries");

            migrationBuilder.DropIndex(
                name: "ix_gis_addon_values_gis_addon_id_report_date",
                table: "gis_addon_values");

            migrationBuilder.DropIndex(
                name: "ix_file_type_settings_name",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "file_date",
                table: "input_file_logs");

            migrationBuilder.DropColumn(
                name: "file_time",
                table: "input_file_logs");

            migrationBuilder.DropColumn(
                name: "input_time",
                table: "input_file_logs");

            migrationBuilder.DropColumn(
                name: "gis_input_names",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "gis_output_names",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "names",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "report_date",
                table: "gis_output_values");

            migrationBuilder.DropColumn(
                name: "report_date",
                table: "gis_input_values");

            migrationBuilder.DropColumn(
                name: "report_date",
                table: "gis_country_values");

            migrationBuilder.DropColumn(
                name: "is_input",
                table: "gis_addons");

            migrationBuilder.DropColumn(
                name: "names",
                table: "gis_addons");

            migrationBuilder.DropColumn(
                name: "report_date",
                table: "gis_addon_values");

            migrationBuilder.DropColumn(
                name: "type_name",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "names",
                table: "countries");

            migrationBuilder.RenameColumn(
                name: "is_top",
                table: "gises",
                newName: "show_on_top");

            migrationBuilder.RenameColumn(
                name: "is_no_phg",
                table: "gises",
                newName: "show_on_bottom");

            migrationBuilder.RenameColumn(
                name: "is_bottom",
                table: "gises",
                newName: "no_phg");

            migrationBuilder.RenameColumn(
                name: "requested_value",
                table: "gis_output_values",
                newName: "requsted_value");

            migrationBuilder.RenameColumn(
                name: "requested_value",
                table: "gis_input_values",
                newName: "requsted_value");

            migrationBuilder.RenameColumn(
                name: "requested_value",
                table: "gis_country_values",
                newName: "requsted_value");

            migrationBuilder.RenameColumn(
                name: "is_not_calculated",
                table: "gis_countries",
                newName: "is_calculated");

            migrationBuilder.RenameColumn(
                name: "is_output",
                table: "gis_addons",
                newName: "is_calculated");

            migrationBuilder.RenameColumn(
                name: "requested_value",
                table: "gis_addon_values",
                newName: "requsted_value");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "rating_internals",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "rating_externals",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "rating_countries",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "input_file_logs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_file",
                table: "input_file_logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "time_file",
                table: "input_file_logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "time_input",
                table: "input_file_logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "guarantees",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_report",
                table: "guarantee_reports",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_expiration",
                table: "guarantee_reports",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_end",
                table: "guarantee_limits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_agree_start",
                table: "guarantee_limits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_agree_end",
                table: "guarantee_limits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_approval",
                table: "guarantee_approval_docs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_output_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "gis_output_values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_report",
                table: "gis_output_values",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_input_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "gis_input_values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_report",
                table: "gis_input_values",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_country_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "gis_country_values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_report",
                table: "gis_country_values",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "month",
                table: "gis_country_resources",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "multiplicator",
                table: "gis_countries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "multiplicator",
                table: "gis_addons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "requested_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fact_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estimated_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "allocated_value_time_id",
                table: "gis_addon_values",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "gis_addon_values",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_report",
                table: "gis_addon_values",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "financial_statements",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "currency_rates",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "ticker",
                table: "countries",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "counterparties",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_start",
                table: "committees",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "country_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_country_names_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_addon_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_addon_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_addon_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_addon_names_gis_addons_gis_addon_id",
                        column: x => x.gis_addon_id,
                        principalTable: "gis_addons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_input_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_input_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_input_names_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_names_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_output_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_output_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_output_names_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_gis_id",
                table: "gis_output_values",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_gis_id",
                table: "gis_input_values",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_gis_country_id",
                table: "gis_country_values",
                column: "gis_country_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_resources_gis_country_id",
                table: "gis_country_resources",
                column: "gis_country_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_countries_gis_id",
                table: "gis_countries",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_gis_addon_id",
                table: "gis_addon_values",
                column: "gis_addon_id");

            migrationBuilder.CreateIndex(
                name: "ix_country_names_country_id",
                table: "country_names",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_names_gis_addon_id",
                table: "gis_addon_names",
                column: "gis_addon_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_names_gis_id",
                table: "gis_input_names",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_names_gis_id",
                table: "gis_names",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_names_gis_id",
                table: "gis_output_names",
                column: "gis_id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_allocated_value_time_id",
                table: "gis_addon_values",
                column: "allocated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_estimated_value_time_id",
                table: "gis_addon_values",
                column: "estimated_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_fact_value_time_id",
                table: "gis_addon_values",
                column: "fact_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_gis_addon_values_input_file_logs_requested_value_time_id",
                table: "gis_addon_values",
                column: "requested_value_time_id",
                principalTable: "input_file_logs",
                principalColumn: "id");
        }
    }
}
