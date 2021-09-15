using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LogSuite.DataAccess.Migrations
{
    public partial class fileTypeSettings_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "file_type_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    must_have = table.Column<string[]>(type: "text[]", nullable: true),
                    not_have = table.Column<string[]>(type: "text[]", nullable: true),
                    country_entry = table.Column<string[]>(type: "text[]", nullable: true),
                    gis_entry = table.Column<string[]>(type: "text[]", nullable: true),
                    requested_value_entry = table.Column<string[]>(type: "text[]", nullable: true),
                    allocated_value_entry = table.Column<string[]>(type: "text[]", nullable: true),
                    estimated_value_entry = table.Column<string[]>(type: "text[]", nullable: true),
                    fact_value_entry = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file_type_settings", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_type_settings");
        }
    }
}
