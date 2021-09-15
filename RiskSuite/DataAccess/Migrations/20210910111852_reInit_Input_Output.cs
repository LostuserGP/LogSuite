using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LogSuite.DataAccess.Migrations
{
    public partial class reInit_Input_Output : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_names_gis_inputs_gis_input_id",
                table: "gis_input_names");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_gis_inputs_gis_input_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_names_gis_outputs_gis_output_id",
                table: "gis_output_names");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_gis_outputs_gis_output_id",
                table: "gis_output_values");

            migrationBuilder.DropTable(
                name: "gis_inputs");

            migrationBuilder.DropTable(
                name: "gis_outputs");

            migrationBuilder.DropColumn(
                name: "gis_input_id",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "gis_output_id",
                table: "gises");

            migrationBuilder.RenameColumn(
                name: "gis_output_id",
                table: "gis_output_values",
                newName: "gis_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_output_values_gis_output_id",
                table: "gis_output_values",
                newName: "ix_gis_output_values_gis_id");

            migrationBuilder.RenameColumn(
                name: "gis_output_id",
                table: "gis_output_names",
                newName: "gis_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_output_names_gis_output_id",
                table: "gis_output_names",
                newName: "ix_gis_output_names_gis_id");

            migrationBuilder.RenameColumn(
                name: "gis_input_id",
                table: "gis_input_values",
                newName: "gis_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_input_values_gis_input_id",
                table: "gis_input_values",
                newName: "ix_gis_input_values_gis_id");

            migrationBuilder.RenameColumn(
                name: "gis_input_id",
                table: "gis_input_names",
                newName: "gis_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_input_names_gis_input_id",
                table: "gis_input_names",
                newName: "ix_gis_input_names_gis_id");

            migrationBuilder.AddColumn<string>(
                name: "daily_review_name",
                table: "gises",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "show_gis_phg",
                table: "gises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "daily_review_name",
                table: "gis_addons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "daily_review_name",
                table: "countries",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_names_gises_gis_id",
                table: "gis_input_names",
                column: "gis_id",
                principalTable: "gises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_gises_gis_id",
                table: "gis_input_values",
                column: "gis_id",
                principalTable: "gises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_names_gises_gis_id",
                table: "gis_output_names",
                column: "gis_id",
                principalTable: "gises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_gises_gis_id",
                table: "gis_output_values",
                column: "gis_id",
                principalTable: "gises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_names_gises_gis_id",
                table: "gis_input_names");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_input_values_gises_gis_id",
                table: "gis_input_values");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_names_gises_gis_id",
                table: "gis_output_names");

            migrationBuilder.DropForeignKey(
                name: "fk_gis_output_values_gises_gis_id",
                table: "gis_output_values");

            migrationBuilder.DropColumn(
                name: "daily_review_name",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "show_gis_phg",
                table: "gises");

            migrationBuilder.DropColumn(
                name: "daily_review_name",
                table: "gis_addons");

            migrationBuilder.DropColumn(
                name: "daily_review_name",
                table: "countries");

            migrationBuilder.RenameColumn(
                name: "gis_id",
                table: "gis_output_values",
                newName: "gis_output_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_output_values_gis_id",
                table: "gis_output_values",
                newName: "ix_gis_output_values_gis_output_id");

            migrationBuilder.RenameColumn(
                name: "gis_id",
                table: "gis_output_names",
                newName: "gis_output_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_output_names_gis_id",
                table: "gis_output_names",
                newName: "ix_gis_output_names_gis_output_id");

            migrationBuilder.RenameColumn(
                name: "gis_id",
                table: "gis_input_values",
                newName: "gis_input_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_input_values_gis_id",
                table: "gis_input_values",
                newName: "ix_gis_input_values_gis_input_id");

            migrationBuilder.RenameColumn(
                name: "gis_id",
                table: "gis_input_names",
                newName: "gis_input_id");

            migrationBuilder.RenameIndex(
                name: "ix_gis_input_names_gis_id",
                table: "gis_input_names",
                newName: "ix_gis_input_names_gis_input_id");

            migrationBuilder.AddColumn<int>(
                name: "gis_input_id",
                table: "gises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "gis_output_id",
                table: "gises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "gis_inputs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
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
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    multiplicator = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "ix_gis_inputs_gis_id",
                table: "gis_inputs",
                column: "gis_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_outputs_gis_id",
                table: "gis_outputs",
                column: "gis_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_names_gis_inputs_gis_input_id",
                table: "gis_input_names",
                column: "gis_input_id",
                principalTable: "gis_inputs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_input_values_gis_inputs_gis_input_id",
                table: "gis_input_values",
                column: "gis_input_id",
                principalTable: "gis_inputs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_names_gis_outputs_gis_output_id",
                table: "gis_output_names",
                column: "gis_output_id",
                principalTable: "gis_outputs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_gis_output_values_gis_outputs_gis_output_id",
                table: "gis_output_values",
                column: "gis_output_id",
                principalTable: "gis_outputs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
