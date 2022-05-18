using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogSuite.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "committee_limits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_committee_limits", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "committee_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_committee_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "counterparty_groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_counterparty_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    daily_review_name = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true),
                    short_name = table.Column<string>(type: "text", nullable: true),
                    names = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.id);
                });

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
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    short_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "file_type_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    type_name = table.Column<string>(type: "text", nullable: true),
                    must_have = table.Column<List<string>>(type: "text[]", nullable: true),
                    not_have = table.Column<List<string>>(type: "text[]", nullable: true),
                    country_entry = table.Column<List<string>>(type: "text[]", nullable: true),
                    gis_entry = table.Column<List<string>>(type: "text[]", nullable: true),
                    requested_value_entry = table.Column<List<string>>(type: "text[]", nullable: true),
                    allocated_value_entry = table.Column<List<string>>(type: "text[]", nullable: true),
                    estimated_value_entry = table.Column<List<string>>(type: "text[]", nullable: true),
                    fact_value_entry = table.Column<List<string>>(type: "text[]", nullable: true),
                    data_entry = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file_type_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "financial_sectors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financial_sectors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "financial_statement_standards",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financial_statement_standards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    daily_review_name = table.Column<string>(type: "text", nullable: true),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    is_ukraine_transport = table.Column<bool>(type: "boolean", nullable: false),
                    is_not_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    is_top = table.Column<bool>(type: "boolean", nullable: false),
                    is_bottom = table.Column<bool>(type: "boolean", nullable: false),
                    is_one_row = table.Column<bool>(type: "boolean", nullable: false),
                    is_no_phg = table.Column<bool>(type: "boolean", nullable: false),
                    names = table.Column<List<string>>(type: "text[]", nullable: true),
                    gis_input_names = table.Column<List<string>>(type: "text[]", nullable: true),
                    gis_output_names = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "guarantee_approval_doc_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantee_approval_doc_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "guarantee_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantee_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "portfolios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_portfolios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rating_agencies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rating_agencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rating_groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    group_limit = table.Column<long>(type: "bigint", nullable: false),
                    group_limit_bank = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rating_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "risk_classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_risk_classes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subsidiaries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subsidiaries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "currency_rates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency_from_id = table.Column<int>(type: "integer", nullable: false),
                    currency_to_id = table.Column<int>(type: "integer", nullable: false),
                    rate = table.Column<float>(type: "real", nullable: false),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_rates", x => x.id);
                    table.ForeignKey(
                        name: "fk_currency_rates_currencies_currency_from_id",
                        column: x => x.currency_from_id,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currency_rates_currencies_currency_to_id",
                        column: x => x.currency_to_id,
                        principalTable: "currencies",
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
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    custom_claim = table.Column<string>(type: "text", nullable: true),
                    department_id = table.Column<int>(type: "integer", nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_users_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "counterparties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    short_name = table.Column<string>(type: "text", nullable: true),
                    is_intra_group = table.Column<bool>(type: "boolean", nullable: false),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    is_monitored = table.Column<bool>(type: "boolean", nullable: false),
                    ticker = table.Column<string>(type: "text", nullable: true),
                    inn = table.Column<string>(type: "text", nullable: true),
                    swift = table.Column<string>(type: "text", nullable: true),
                    bank_class = table.Column<string>(type: "text", nullable: true),
                    financial_sector_id = table.Column<int>(type: "integer", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    country_risk_id = table.Column<int>(type: "integer", nullable: true),
                    rating_donor_id = table.Column<int>(type: "integer", nullable: true),
                    counterparty_group_id = table.Column<int>(type: "integer", nullable: true),
                    gtc = table.Column<string>(type: "text", nullable: true),
                    is_long_term = table.Column<bool>(type: "boolean", nullable: false),
                    is_etp = table.Column<bool>(type: "boolean", nullable: false),
                    is_efet = table.Column<bool>(type: "boolean", nullable: false),
                    causes = table.Column<string>(type: "text", nullable: true),
                    is_srk = table.Column<bool>(type: "boolean", nullable: false),
                    duns = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_counterparties", x => x.id);
                    table.ForeignKey(
                        name: "fk_counterparties_counterparties_rating_donor_id",
                        column: x => x.rating_donor_id,
                        principalTable: "counterparties",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_counterparties_counterparty_groups_counterparty_group_id",
                        column: x => x.counterparty_group_id,
                        principalTable: "counterparty_groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_counterparties_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_counterparties_countries_country_risk_id",
                        column: x => x.country_risk_id,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_counterparties_financial_sectors_financial_sector_id",
                        column: x => x.financial_sector_id,
                        principalTable: "financial_sectors",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "gis_addons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    daily_review_name = table.Column<string>(type: "text", nullable: true),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    is_input = table.Column<bool>(type: "boolean", nullable: false),
                    is_output = table.Column<bool>(type: "boolean", nullable: false),
                    names = table.Column<List<string>>(type: "text[]", nullable: true)
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
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    is_hidden = table.Column<bool>(type: "boolean", nullable: false),
                    is_not_calculated = table.Column<bool>(type: "boolean", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_countries_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    score = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    rating_group_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ratings", x => x.id);
                    table.ForeignKey(
                        name: "fk_ratings_rating_groups_rating_group_id",
                        column: x => x.rating_group_id,
                        principalTable: "rating_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "input_file_logs",
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
                    table.PrimaryKey("pk_input_file_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_input_file_logs_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "committees",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    counterparty_id = table.Column<int>(type: "integer", nullable: false),
                    committee_status_id = table.Column<int>(type: "integer", nullable: false),
                    committee_limit_id = table.Column<int>(type: "integer", nullable: true),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_committees", x => x.id);
                    table.ForeignKey(
                        name: "fk_committees_committee_limits_committee_limit_id",
                        column: x => x.committee_limit_id,
                        principalTable: "committee_limits",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_committees_committee_statuses_committee_status_id",
                        column: x => x.committee_status_id,
                        principalTable: "committee_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_committees_counterparties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "counterparty_portfolios",
                columns: table => new
                {
                    counterparty_id = table.Column<int>(type: "integer", nullable: false),
                    portfolio_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_counterparty_portfolios", x => new { x.counterparty_id, x.portfolio_id });
                    table.ForeignKey(
                        name: "fk_counterparty_portfolios_counterparties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_counterparty_portfolios_portfolios_portfolio_id",
                        column: x => x.portfolio_id,
                        principalTable: "portfolios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "financial_statements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    counterparty_id = table.Column<int>(type: "integer", nullable: false),
                    financial_statement_standard_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financial_statements", x => x.id);
                    table.ForeignKey(
                        name: "fk_financial_statements_counterparties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_financial_statements_financial_statement_standards_financia",
                        column: x => x.financial_statement_standard_id,
                        principalTable: "financial_statement_standards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guarantee_limits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guarantor_id = table.Column<int>(type: "integer", nullable: false),
                    date_agree_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_agree_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false),
                    guarantee_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantee_limits", x => x.id);
                    table.ForeignKey(
                        name: "fk_guarantee_limits_counterparties_guarantor_id",
                        column: x => x.guarantor_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_guarantee_limits_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_guarantee_limits_guarantee_types_guarantee_type_id",
                        column: x => x.guarantee_type_id,
                        principalTable: "guarantee_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guarantees",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "text", nullable: true),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount_initial = table.Column<long>(type: "bigint", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: true),
                    counterparty_id = table.Column<int>(type: "integer", nullable: false),
                    guarantor_id = table.Column<int>(type: "integer", nullable: false),
                    guarantee_type_id = table.Column<int>(type: "integer", nullable: true),
                    beneficiar_id = table.Column<int>(type: "integer", nullable: false),
                    subsidiary_id = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantees", x => x.id);
                    table.ForeignKey(
                        name: "fk_guarantees_counterparties_beneficiar_id",
                        column: x => x.beneficiar_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_guarantees_counterparties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_guarantees_counterparties_guarantor_id",
                        column: x => x.guarantor_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_guarantees_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_guarantees_guarantee_types_guarantee_type_id",
                        column: x => x.guarantee_type_id,
                        principalTable: "guarantee_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_guarantees_subsidiaries_subsidiary_id",
                        column: x => x.subsidiary_id,
                        principalTable: "subsidiaries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "gis_country_resources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_country_id = table.Column<int>(type: "integer", nullable: false),
                    month = table.Column<DateOnly>(type: "date", nullable: false),
                    value = table.Column<decimal>(type: "numeric(16,8)", nullable: false)
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
                name: "rating_countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rating_agency_id = table.Column<int>(type: "integer", nullable: false),
                    rating_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rating_countries", x => x.id);
                    table.ForeignKey(
                        name: "fk_rating_countries_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rating_countries_rating_agencies_rating_agency_id",
                        column: x => x.rating_agency_id,
                        principalTable: "rating_agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rating_countries_ratings_rating_id",
                        column: x => x.rating_id,
                        principalTable: "ratings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rating_externals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    counterparty_id = table.Column<int>(type: "integer", nullable: false),
                    rating_agency_id = table.Column<int>(type: "integer", nullable: false),
                    rating_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rating_externals", x => x.id);
                    table.ForeignKey(
                        name: "fk_rating_externals_counterparties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rating_externals_rating_agencies_rating_agency_id",
                        column: x => x.rating_agency_id,
                        principalTable: "rating_agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rating_externals_ratings_rating_id",
                        column: x => x.rating_id,
                        principalTable: "ratings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gis_addon_values",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_addon_id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_gis_addon_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_gis_addons_gis_addon_id",
                        column: x => x.gis_addon_id,
                        principalTable: "gis_addons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_log_dto_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_log_dto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_log_dto_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_log_dto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_log_dto_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_log_dto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_addon_values_input_file_log_dto_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_log_dto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "gis_country_values",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_country_id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_gis_country_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_country_values_gis_countries_gis_country_id",
                        column: x => x.gis_country_id,
                        principalTable: "gis_countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_country_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "gis_input_values",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_gis_input_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_input_values_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_input_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "gis_output_values",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gis_id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_gis_output_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_gis_output_values_gises_gis_id",
                        column: x => x.gis_id,
                        principalTable: "gises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_allocated_value_time_id",
                        column: x => x.allocated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_estimated_value_time_id",
                        column: x => x.estimated_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_fact_value_time_id",
                        column: x => x.fact_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gis_output_values_input_file_logs_requested_value_time_id",
                        column: x => x.requested_value_time_id,
                        principalTable: "input_file_logs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rating_internals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_conservative = table.Column<bool>(type: "boolean", nullable: false),
                    analyst = table.Column<string>(type: "text", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    counterparty_id = table.Column<int>(type: "integer", nullable: false),
                    rating_id = table.Column<int>(type: "integer", nullable: false),
                    rating_wc_id = table.Column<int>(type: "integer", nullable: true),
                    risk_class_id = table.Column<int>(type: "integer", nullable: true),
                    financial_statement_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rating_internals", x => x.id);
                    table.ForeignKey(
                        name: "fk_rating_internals_counterparties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "counterparties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rating_internals_financial_statements_financial_statement_id",
                        column: x => x.financial_statement_id,
                        principalTable: "financial_statements",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_rating_internals_ratings_rating_id",
                        column: x => x.rating_id,
                        principalTable: "ratings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rating_internals_ratings_rating_wc_id",
                        column: x => x.rating_wc_id,
                        principalTable: "ratings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_rating_internals_risk_classes_risk_class_id",
                        column: x => x.risk_class_id,
                        principalTable: "risk_classes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "guarantee_approval_docs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guarantee_id = table.Column<int>(type: "integer", nullable: false),
                    date_approval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    guarantee_approval_doc_type_id = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantee_approval_docs", x => x.id);
                    table.ForeignKey(
                        name: "fk_guarantee_approval_docs_guarantee_approval_doc_types_guaran",
                        column: x => x.guarantee_approval_doc_type_id,
                        principalTable: "guarantee_approval_doc_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_guarantee_approval_docs_guarantees_guarantee_id",
                        column: x => x.guarantee_id,
                        principalTable: "guarantees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guarantee_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guarantee_id = table.Column<int>(type: "integer", nullable: false),
                    date_report = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    amount_operation = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guarantee_reports", x => x.id);
                    table.ForeignKey(
                        name: "fk_guarantee_reports_guarantees_guarantee_id",
                        column: x => x.guarantee_id,
                        principalTable: "guarantees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_department_id",
                table: "AspNetUsers",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_committees_committee_limit_id",
                table: "committees",
                column: "committee_limit_id");

            migrationBuilder.CreateIndex(
                name: "ix_committees_committee_status_id",
                table: "committees",
                column: "committee_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_committees_counterparty_id",
                table: "committees",
                column: "counterparty_id");

            migrationBuilder.CreateIndex(
                name: "ix_counterparties_counterparty_group_id",
                table: "counterparties",
                column: "counterparty_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_counterparties_country_id",
                table: "counterparties",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_counterparties_country_risk_id",
                table: "counterparties",
                column: "country_risk_id");

            migrationBuilder.CreateIndex(
                name: "ix_counterparties_financial_sector_id",
                table: "counterparties",
                column: "financial_sector_id");

            migrationBuilder.CreateIndex(
                name: "ix_counterparties_rating_donor_id",
                table: "counterparties",
                column: "rating_donor_id");

            migrationBuilder.CreateIndex(
                name: "ix_counterparty_portfolios_portfolio_id",
                table: "counterparty_portfolios",
                column: "portfolio_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_currency_from_id",
                table: "currency_rates",
                column: "currency_from_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_currency_to_id",
                table: "currency_rates",
                column: "currency_to_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_type_settings_name",
                table: "file_type_settings",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_financial_statements_counterparty_id",
                table: "financial_statements",
                column: "counterparty_id");

            migrationBuilder.CreateIndex(
                name: "ix_financial_statements_financial_statement_standard_id",
                table: "financial_statements",
                column: "financial_statement_standard_id");

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
                name: "ix_gis_addon_values_gis_addon_id_report_date",
                table: "gis_addon_values",
                columns: new[] { "gis_addon_id", "report_date" },
                unique: true);

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
                name: "ix_gis_countries_gis_id_country_id",
                table: "gis_countries",
                columns: new[] { "gis_id", "country_id" },
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
                name: "ix_gis_country_resources_gis_country_id_month",
                table: "gis_country_resources",
                columns: new[] { "gis_country_id", "month" },
                unique: true);

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
                name: "ix_gis_country_values_gis_country_id_report_date",
                table: "gis_country_values",
                columns: new[] { "gis_country_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_country_values_requested_value_time_id",
                table: "gis_country_values",
                column: "requested_value_time_id");

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
                name: "ix_gis_input_values_gis_id_report_date",
                table: "gis_input_values",
                columns: new[] { "gis_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_input_values_requested_value_time_id",
                table: "gis_input_values",
                column: "requested_value_time_id");

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
                name: "ix_gis_output_values_gis_id_report_date",
                table: "gis_output_values",
                columns: new[] { "gis_id", "report_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gis_output_values_requested_value_time_id",
                table: "gis_output_values",
                column: "requested_value_time_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantee_approval_docs_guarantee_approval_doc_type_id",
                table: "guarantee_approval_docs",
                column: "guarantee_approval_doc_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantee_approval_docs_guarantee_id",
                table: "guarantee_approval_docs",
                column: "guarantee_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantee_limits_currency_id",
                table: "guarantee_limits",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantee_limits_guarantee_type_id",
                table: "guarantee_limits",
                column: "guarantee_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantee_limits_guarantor_id",
                table: "guarantee_limits",
                column: "guarantor_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantee_reports_guarantee_id",
                table: "guarantee_reports",
                column: "guarantee_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_beneficiar_id",
                table: "guarantees",
                column: "beneficiar_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_counterparty_id",
                table: "guarantees",
                column: "counterparty_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_currency_id",
                table: "guarantees",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_guarantee_type_id",
                table: "guarantees",
                column: "guarantee_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_guarantor_id",
                table: "guarantees",
                column: "guarantor_id");

            migrationBuilder.CreateIndex(
                name: "ix_guarantees_subsidiary_id",
                table: "guarantees",
                column: "subsidiary_id");

            migrationBuilder.CreateIndex(
                name: "ix_input_file_log_dto_user_id",
                table: "input_file_log_dto",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_input_file_logs_user_id",
                table: "input_file_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_countries_country_id",
                table: "rating_countries",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_countries_rating_agency_id",
                table: "rating_countries",
                column: "rating_agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_countries_rating_id",
                table: "rating_countries",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_externals_counterparty_id",
                table: "rating_externals",
                column: "counterparty_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_externals_rating_agency_id",
                table: "rating_externals",
                column: "rating_agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_externals_rating_id",
                table: "rating_externals",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_internals_counterparty_id",
                table: "rating_internals",
                column: "counterparty_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_internals_financial_statement_id",
                table: "rating_internals",
                column: "financial_statement_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_internals_rating_id",
                table: "rating_internals",
                column: "rating_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_internals_rating_wc_id",
                table: "rating_internals",
                column: "rating_wc_id");

            migrationBuilder.CreateIndex(
                name: "ix_rating_internals_risk_class_id",
                table: "rating_internals",
                column: "risk_class_id");

            migrationBuilder.CreateIndex(
                name: "ix_ratings_rating_group_id",
                table: "ratings",
                column: "rating_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_dto_department_id",
                table: "user_dto",
                column: "department_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "committees");

            migrationBuilder.DropTable(
                name: "counterparty_portfolios");

            migrationBuilder.DropTable(
                name: "currency_rates");

            migrationBuilder.DropTable(
                name: "file_type_settings");

            migrationBuilder.DropTable(
                name: "gis_addon_values");

            migrationBuilder.DropTable(
                name: "gis_country_addon_types");

            migrationBuilder.DropTable(
                name: "gis_country_addon_values");

            migrationBuilder.DropTable(
                name: "gis_country_resources");

            migrationBuilder.DropTable(
                name: "gis_country_values");

            migrationBuilder.DropTable(
                name: "gis_input_values");

            migrationBuilder.DropTable(
                name: "gis_output_values");

            migrationBuilder.DropTable(
                name: "guarantee_approval_docs");

            migrationBuilder.DropTable(
                name: "guarantee_limits");

            migrationBuilder.DropTable(
                name: "guarantee_reports");

            migrationBuilder.DropTable(
                name: "rating_countries");

            migrationBuilder.DropTable(
                name: "rating_externals");

            migrationBuilder.DropTable(
                name: "rating_internals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "committee_limits");

            migrationBuilder.DropTable(
                name: "committee_statuses");

            migrationBuilder.DropTable(
                name: "portfolios");

            migrationBuilder.DropTable(
                name: "gis_addons");

            migrationBuilder.DropTable(
                name: "input_file_log_dto");

            migrationBuilder.DropTable(
                name: "gis_country_addons");

            migrationBuilder.DropTable(
                name: "input_file_logs");

            migrationBuilder.DropTable(
                name: "guarantee_approval_doc_types");

            migrationBuilder.DropTable(
                name: "guarantees");

            migrationBuilder.DropTable(
                name: "rating_agencies");

            migrationBuilder.DropTable(
                name: "financial_statements");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "risk_classes");

            migrationBuilder.DropTable(
                name: "user_dto");

            migrationBuilder.DropTable(
                name: "gis_countries");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "guarantee_types");

            migrationBuilder.DropTable(
                name: "subsidiaries");

            migrationBuilder.DropTable(
                name: "counterparties");

            migrationBuilder.DropTable(
                name: "financial_statement_standards");

            migrationBuilder.DropTable(
                name: "rating_groups");

            migrationBuilder.DropTable(
                name: "department_dto");

            migrationBuilder.DropTable(
                name: "gises");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "counterparty_groups");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "financial_sectors");
        }
    }
}
