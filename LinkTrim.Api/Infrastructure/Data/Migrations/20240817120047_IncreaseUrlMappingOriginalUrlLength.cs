using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkTrim.Api.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class IncreaseUrlMappingOriginalUrlLength : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OriginalUrl",
            table: "UrlMappings",
            type: "nvarchar(1000)",
            maxLength: 1000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OriginalUrl",
            table: "UrlMappings",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(1000)",
            oldMaxLength: 1000);
    }
}
