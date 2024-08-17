using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkTrim.Api.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class IncreaseUrlMappingOriginalUrlLength1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OriginalUrl",
            table: "UrlMappings",
            type: "nvarchar(4000)",
            maxLength: 4000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(8000)",
            oldMaxLength: 8000);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OriginalUrl",
            table: "UrlMappings",
            type: "nvarchar(8000)",
            maxLength: 8000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(4000)",
            oldMaxLength: 4000);
    }
}
