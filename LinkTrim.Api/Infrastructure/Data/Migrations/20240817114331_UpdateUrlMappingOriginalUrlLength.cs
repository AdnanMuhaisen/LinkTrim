using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkTrim.Api.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class UpdateUrlMappingOriginalUrlLength : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OriginalUrl",
            table: "UrlMappings",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "OriginalUrl",
            table: "UrlMappings",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }
}
