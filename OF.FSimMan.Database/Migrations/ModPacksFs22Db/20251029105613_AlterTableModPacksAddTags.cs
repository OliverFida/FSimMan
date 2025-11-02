using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OF.FSimMan.Database.Migrations.ModPacksFs22Db
{
    /// <inheritdoc />
    public partial class AlterTableModPacksAddTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "ModPacks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "ModPacks");
        }
    }
}
