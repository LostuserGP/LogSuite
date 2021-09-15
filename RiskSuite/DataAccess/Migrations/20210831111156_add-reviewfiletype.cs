using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LogSuite.DataAccess.Migrations
{
    public partial class addreviewfiletype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "review_file_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
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
                    name = table.Column<string>(type: "text", nullable: true),
                    must_have_name_id = table.Column<int>(type: "integer", nullable: true),
                    not_have_name_id = table.Column<int>(type: "integer", nullable: true),
                    country_name_entry_id = table.Column<int>(type: "integer", nullable: true),
                    gis_name_entry_id = table.Column<int>(type: "integer", nullable: true),
                    requested_value_entry_id = table.Column<int>(type: "integer", nullable: true),
                    allocated_value_entry_id = table.Column<int>(type: "integer", nullable: true),
                    estimated_value_entry_id = table.Column<int>(type: "integer", nullable: true),
                    fact_value_entry_id = table.Column<int>(type: "integer", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "review_file_type_names");

            migrationBuilder.DropTable(
                name: "review_file_types");
        }
    }
}
