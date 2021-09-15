using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LogSuite.DataAccess.Migrations
{
    public partial class addGisInputAndOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gis_phg_values");

            migrationBuilder.DropTable(
                name: "review_file_type_names");

            migrationBuilder.DropTable(
                name: "review_file_types");

            migrationBuilder.DropColumn(
                name: "is_calculated",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "month_number",
                table: "gis_country_resources");

            migrationBuilder.DropColumn(
                name: "strict_allocated_value_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_country_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_data_entry",
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

            migrationBuilder.RenameColumn(
                name: "multiplicator",
                table: "gises",
                newName: "gis_output_id");

            migrationBuilder.RenameColumn(
                name: "is_excluded",
                table: "gises",
                newName: "is_hidden");

            migrationBuilder.RenameColumn(
                name: "is_excluded",
                table: "gis_countries",
                newName: "is_hidden");

            migrationBuilder.AddColumn<int>(
                name: "gis_input_id",
                table: "gises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "month",
                table: "gis_country_resources",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_hidden",
                table: "gis_addons",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "gis_inputs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_inputs", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_inputs_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_outputs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_outputs", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_outputs_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_input_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_input_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_input_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_input_names_gis_inputs_gis_input_id",
                        column: x => x.gis_input_id,
                        principalTable: "gis_inputs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_input_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_input_id = table.Column<int>(type: "integer", nullable: false),
                    date_report = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    requsted_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    requested_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    allocated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    allocated_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    estimated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    estimated_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    fact_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    fact_value_time_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_input_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_input_values_gis_inputs_gis_input_id",
                        column: x => x.gis_input_id,
                        principalTable: "gis_inputs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_output_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_output_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_output_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_output_names_gis_outputs_gis_output_id",
                        column: x => x.gis_output_id,
                        principalTable: "gis_outputs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_output_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_output_id = table.Column<int>(type: "integer", nullable: false),
                    date_report = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    requsted_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    requested_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    allocated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    allocated_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    estimated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    estimated_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    fact_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    fact_value_time_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_output_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_output_values_gis_outputs_gis_output_id",
                        column: x => x.gis_output_id,
                        principalTable: "gis_outputs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_names_gis_input_id",
                table: "gis_input_names",
                column: "gis_input_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_allocated_value_time_id",
                table: "gis_input_values",
                column: "allocated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_estimated_value_time_id",
                table: "gis_input_values",
                column: "estimated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_fact_value_time_id",
                table: "gis_input_values",
                column: "fact_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_gis_input_id",
                table: "gis_input_values",
                column: "gis_input_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_requested_value_time_id",
                table: "gis_input_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_inputs_gis_id",
                table: "gis_inputs",
                column: "gis_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_names_gis_output_id",
                table: "gis_output_names",
                column: "gis_output_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_allocated_value_time_id",
                table: "gis_output_values",
                column: "allocated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_estimated_value_time_id",
                table: "gis_output_values",
                column: "estimated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_fact_value_time_id",
                table: "gis_output_values",
                column: "fact_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_gis_output_id",
                table: "gis_output_values",
                column: "gis_output_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_requested_value_time_id",
                table: "gis_output_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_outputs_gis_id",
                table: "gis_outputs",
                column: "gis_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gis_input_names");

            migrationBuilder.DropTable(
                name: "gis_input_values");

            migrationBuilder.DropTable(
                name: "gis_output_names");

            migrationBuilder.DropTable(
                name: "gis_output_values");

            migrationBuilder.DropTable(
                name: "gis_inputs");

            migrationBuilder.DropTable(
                name: "gis_outputs");

            migrationBuilder.DropColumn(
                name: "gis_input_id",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "month",
                table: "gis_country_resources");

            migrationBuilder.DropColumn(
                name: "is_hidden",
                table: "gis_addons");

            migrationBuilder.RenameColumn(
                name: "is_hidden",
                table: "gises",
                newName: "is_excluded");

            migrationBuilder.RenameColumn(
                name: "gis_output_id",
                table: "gises",
                newName: "multiplicator");

            migrationBuilder.RenameColumn(
                name: "is_hidden",
                table: "gis_countries",
                newName: "is_excluded");

            migrationBuilder.AddColumn<bool>(
                name: "is_calculated",
                table: "gises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "month_number",
                table: "gis_country_resources",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                name: "strict_data_entry",
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

            migrationBuilder.CreateTable(
                name: "gis_phg_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    allocated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    allocated_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    date_report = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    estimated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    estimated_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    fact_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    fact_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    is_input = table.Column<bool>(type: "boolean", nullable: false),
                    requested_value_time_id = table.Column<int>(type: "integer", nullable: false),
                    requsted_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_phg_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review_file_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    strict_allocated_value_entries = table.Column<bool>(type: "boolean", nullable: false),
                    strict_country_name_entries = table.Column<bool>(type: "boolean", nullable: false),
                    strict_estimated_value_entries = table.Column<bool>(type: "boolean", nullable: false),
                    strict_fact_value_entries = table.Column<bool>(type: "boolean", nullable: false),
                    strict_gis_name_entries = table.Column<bool>(type: "boolean", nullable: false),
                    strict_must_have_names = table.Column<bool>(type: "boolean", nullable: false),
                    strict_not_have_names = table.Column<bool>(type: "boolean", nullable: false),
                    strict_requested_value_entries = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_review_file_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "review_file_type_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    allocated_value_entry_id = table.Column<int>(type: "integer", nullable: true),
                    country_name_entry_id = table.Column<int>(type: "integer", nullable: true),
                    estimated_value_entry_id = table.Column<int>(type: "integer", nullable: true),
                    fact_value_entry_id = table.Column<int>(type: "integer", nullable: true),
                    gis_name_entry_id = table.Column<int>(type: "integer", nullable: true),
                    must_have_name_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    not_have_name_id = table.Column<int>(type: "integer", nullable: true),
                    requested_value_entry_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_review_file_type_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_allocated_value_en",
                        column: x => x.allocated_value_entry_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_country_name_entry",
                        column: x => x.country_name_entry_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_estimated_value_en",
                        column: x => x.estimated_value_entry_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_fact_value_entry_id",
                        column: x => x.fact_value_entry_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_gis_name_entry_id",
                        column: x => x.gis_name_entry_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_must_have_name_id",
                        column: x => x.must_have_name_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_not_have_name_id",
                        column: x => x.not_have_name_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_review_file_type_names_review_file_types_requested_value_en",
                        column: x => x.requested_value_entry_id,
                        principalTable: "review_file_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gis_phg_values_allocated_value_time_id",
                table: "gis_phg_values",
                column: "allocated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_phg_values_estimated_value_time_id",
                table: "gis_phg_values",
                column: "estimated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_phg_values_fact_value_time_id",
                table: "gis_phg_values",
                column: "fact_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_phg_values_gis_id",
                table: "gis_phg_values",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_phg_values_requested_value_time_id",
                table: "gis_phg_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_allocated_value_entry_id",
                table: "review_file_type_names",
                column: "allocated_value_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_country_name_entry_id",
                table: "review_file_type_names",
                column: "country_name_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_estimated_value_entry_id",
                table: "review_file_type_names",
                column: "estimated_value_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_fact_value_entry_id",
                table: "review_file_type_names",
                column: "fact_value_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_gis_name_entry_id",
                table: "review_file_type_names",
                column: "gis_name_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_must_have_name_id",
                table: "review_file_type_names",
                column: "must_have_name_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_not_have_name_id",
                table: "review_file_type_names",
                column: "not_have_name_id");

            migrationBuilder.CreateIndex(
                name: "ix_review_file_type_names_requested_value_entry_id",
                table: "review_file_type_names",
                column: "requested_value_entry_id");
        }
    }
}
