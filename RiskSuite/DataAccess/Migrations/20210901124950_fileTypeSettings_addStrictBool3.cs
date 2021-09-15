using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LogSuite.DataAccess.Migrations
{
    public partial class fileTypeSettings_addStrictBool3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "data_entry",
                table: "file_type_settings",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "strict_data_entry",
                table: "file_type_settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_entry",
                table: "file_type_settings");

            migrationBuilder.DropColumn(
                name: "strict_data_entry",
                table: "file_type_settings");
        }
    }
}
