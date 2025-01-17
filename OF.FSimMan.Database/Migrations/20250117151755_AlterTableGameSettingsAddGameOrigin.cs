using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OF.FSimMan.Database.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableGameSettingsAddGameOrigin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameOrigin",
                table: "GameSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameOrigin",
                table: "GameSettings");
        }
    }
}
