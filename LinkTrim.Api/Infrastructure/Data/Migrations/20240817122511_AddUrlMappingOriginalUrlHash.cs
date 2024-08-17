using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkTrim.Api.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddUrlMappingOriginalUrlHash : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_UrlMappings_OriginalUrl",
            table: "UrlMappings");

        migrationBuilder.AddColumn<string>(
            name: "OriginalUrlHash",
            table: "UrlMappings",
            type: "nvarchar(450)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "IX_UrlMappings_OriginalUrlHash",
            table: "UrlMappings",
            column: "OriginalUrlHash");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_UrlMappings_OriginalUrlHash",
            table: "UrlMappings");

        migrationBuilder.DropColumn(
            name: "OriginalUrlHash",
            table: "UrlMappings");

        migrationBuilder.CreateIndex(
            name: "IX_UrlMappings_OriginalUrl",
            table: "UrlMappings",
            column: "OriginalUrl");
    }
}
