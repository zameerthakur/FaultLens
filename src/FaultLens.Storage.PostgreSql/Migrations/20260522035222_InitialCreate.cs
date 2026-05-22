using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaultLens.Storage.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exception_groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fingerprint = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ExceptionType = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    OccurrenceCount = table.Column<long>(type: "bigint", nullable: false),
                    FirstOccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastOccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exception_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "exception_records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicationName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EnvironmentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExceptionType = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    CorrelationId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TagsJson = table.Column<string>(type: "text", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exception_records", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exception_groups_Category",
                table: "exception_groups",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_exception_groups_Fingerprint",
                table: "exception_groups",
                column: "Fingerprint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_exception_groups_LastOccurredAtUtc",
                table: "exception_groups",
                column: "LastOccurredAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_exception_groups_Severity",
                table: "exception_groups",
                column: "Severity");

            migrationBuilder.CreateIndex(
                name: "IX_exception_records_ApplicationName",
                table: "exception_records",
                column: "ApplicationName");

            migrationBuilder.CreateIndex(
                name: "IX_exception_records_EnvironmentName",
                table: "exception_records",
                column: "EnvironmentName");

            migrationBuilder.CreateIndex(
                name: "IX_exception_records_ExceptionType",
                table: "exception_records",
                column: "ExceptionType");

            migrationBuilder.CreateIndex(
                name: "IX_exception_records_OccurredAtUtc",
                table: "exception_records",
                column: "OccurredAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exception_groups");

            migrationBuilder.DropTable(
                name: "exception_records");
        }
    }
}
