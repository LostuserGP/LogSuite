using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LogSuite.DataAccess.Migrations
{
    public partial class initOp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_country_name_countries_country_id",
                table: "country_name");

            migrationBuilder.DropPrimaryKey(
                name: "pk_country_name",
                table: "country_name");

            migrationBuilder.RenameTable(
                name: "country_name",
                newName: "country_names");

            migrationBuilder.RenameIndex(
                name: "ix_country_name_country_id",
                table: "country_names",
                newName: "ix_country_names_country_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_country_names",
                table: "country_names",
                column: "id");

            migrationBuilder.CreateTable(
                name: "gises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_excluded = table.Column<bool>(type: "boolean", nullable: false),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false),
                    is_ukraine_transport = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "input_file_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    filename = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    time_input = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    date_file = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    time_file = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_input_file_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_input_file_logs_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gis_addons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_addons", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_addons_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    is_excluded = table.Column<bool>(type: "boolean", nullable: false),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false),
                    gis_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_countries", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_countries_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_countries_gises_gis_id",
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
                name: "gis_phg_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    is_input = table.Column<bool>(type: "boolean", nullable: false),
                    date_report = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    requsted_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    requested_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    allocated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    allocated_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    estimated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    estimated_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    fact_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    fact_value_time_id = table.Column<int>(type: "integer", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_phg_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "gis_addon_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_addon_id = table.Column<int>(type: "integer", nullable: false),
                    date_report = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    requsted_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    requested_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    allocated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    allocated_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    estimated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    estimated_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    fact_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    fact_value_time_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_addon_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_gis_addons_gis_addon_id",
                        column: x => x.gis_addon_id,
                        principalTable: "gis_addons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gis_country_resources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_country_id = table.Column<int>(type: "integer", nullable: false),
                    month_number = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<decimal>(type: "numeric(8,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_country_resources", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_country_resources_gis_countries_gis_country_id",
                        column: x => x.gis_country_id,
                        principalTable: "gis_countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_country_values",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    op_gis_country_id = table.Column<int>(type: "integer", nullable: false),
                    gis_country_id = table.Column<int>(type: "integer", nullable: true),
                    report_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    requsted_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    requested_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    allocated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    allocated_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    estimated_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    estimated_value_time_id = table.Column<int>(type: "integer", nullable: true),
                    fact_value = table.Column<decimal>(type: "numeric(8,8)", nullable: false),
                    fact_value_time_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gis_country_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_country_values_gis_countries_gis_country_id",
                        column: x => x.gis_country_id,
                        principalTable: "gis_countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_names_gis_addon_id",
                table: "gis_addon_names",
                column: "gis_addon_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_allocated_value_time_id",
                table: "gis_addon_values",
                column: "allocated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_estimated_value_time_id",
                table: "gis_addon_values",
                column: "estimated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_fact_value_time_id",
                table: "gis_addon_values",
                column: "fact_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_gis_addon_id",
                table: "gis_addon_values",
                column: "gis_addon_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addon_values_requested_value_time_id",
                table: "gis_addon_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_addons_gis_id",
                table: "gis_addons",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_countries_country_id",
                table: "gis_countries",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_countries_gis_id",
                table: "gis_countries",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_resources_gis_country_id",
                table: "gis_country_resources",
                column: "gis_country_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_allocated_value_time_id",
                table: "gis_country_values",
                column: "allocated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_estimated_value_time_id",
                table: "gis_country_values",
                column: "estimated_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_fact_value_time_id",
                table: "gis_country_values",
                column: "fact_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_gis_country_id",
                table: "gis_country_values",
                column: "gis_country_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_requested_value_time_id",
                table: "gis_country_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_gis_names_gis_id",
                table: "gis_names",
                column: "gis_id");

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
                name: "ix_input_file_logs_user_id",
                table: "input_file_logs",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_country_names_countries_country_id",
                table: "country_names",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_country_names_countries_country_id",
                table: "country_names");

            migrationBuilder.DropTable(
                name: "gis_addon_names");

            migrationBuilder.DropTable(
                name: "gis_addon_values");

            migrationBuilder.DropTable(
                name: "gis_country_resources");

            migrationBuilder.DropTable(
                name: "gis_country_values");

            migrationBuilder.DropTable(
                name: "gis_names");

            migrationBuilder.DropTable(
                name: "gis_phg_values");

            migrationBuilder.DropTable(
                name: "gis_addons");

            migrationBuilder.DropTable(
                name: "gis_countries");

            migrationBuilder.DropTable(
                name: "input_file_logs");

            migrationBuilder.DropTable(
                name: "gises");

            migrationBuilder.DropPrimaryKey(
                name: "pk_country_names",
                table: "country_names");

            migrationBuilder.RenameTable(
                name: "country_names",
                newName: "country_name");

            migrationBuilder.RenameIndex(
                name: "ix_country_names_country_id",
                table: "country_name",
                newName: "ix_country_name_country_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_country_name",
                table: "country_name",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_country_name_countries_country_id",
                table: "country_name",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
