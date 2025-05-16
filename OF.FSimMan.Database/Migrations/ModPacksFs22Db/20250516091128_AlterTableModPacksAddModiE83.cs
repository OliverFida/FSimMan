using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OF.FSimMan.Database.Migrations.ModPacksFs22Db
{
    /// <inheritdoc />
    public partial class AlterTableModPacksAddModiE83 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModiE83_IsSyncEnabled",
                table: "ModPacks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModiE83_SyncPath",
                table: "ModPacks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModiE83_IsSyncEnabled",
                table: "ModPacks");

            migrationBuilder.DropColumn(
                name: "ModiE83_SyncPath",
                table: "ModPacks");
        }
    }
}
