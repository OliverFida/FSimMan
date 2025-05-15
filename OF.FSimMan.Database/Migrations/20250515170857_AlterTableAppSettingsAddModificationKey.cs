using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OF.FSimMan.Database.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableAppSettingsAddModificationKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModificationKey",
                table: "AppSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationKey",
                table: "AppSettings");
        }
    }
}
