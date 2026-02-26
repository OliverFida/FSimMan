using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OF.FSimMan.Database.Migrations.ModPacksFs22Db
{
    /// <inheritdoc />
    public partial class MigrationToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Disable Foreign Keys to allow table reconstruction
            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;");

            // 2. Create the New Tables with TEXT (Guid) IDs based on Snapshot
            migrationBuilder.Sql(@"CREATE TABLE ModPacks_New (
        Id TEXT NOT NULL PRIMARY KEY,
        Author TEXT NOT NULL,
        CreatedAt TEXT NOT NULL,
        Description TEXT NOT NULL,
        Game INTEGER NOT NULL,
        Guid TEXT NOT NULL,
        ImageSource TEXT,
        LastModifiedAt TEXT NOT NULL,
        Tags TEXT NOT NULL,
        Title TEXT NOT NULL,
        Version TEXT NOT NULL
    );");

            migrationBuilder.Sql(@"CREATE TABLE Mods_New (
        Id TEXT NOT NULL PRIMARY KEY,
        Author TEXT,
        CreatedAt TEXT NOT NULL,
        Description TEXT,
        FileName TEXT NOT NULL,
        ImageSource TEXT,
        IsMultiplayerCompatible INTEGER NOT NULL,
        LastModifiedAt TEXT NOT NULL,
        ModPackId TEXT NOT NULL,
        Title TEXT NOT NULL,
        Version TEXT,
        FOREIGN KEY (ModPackId) REFERENCES ModPacks_New (Id) ON DELETE CASCADE
    );");

            // 3. Helper for GUID generation (SQLite compatible)
            string guidSql = "upper(hex(randomblob(4))) || '-' || upper(hex(randomblob(2))) || '-4' || substr(upper(hex(randomblob(2))),2) || '-' || substr('89AB', abs(random()) % 4 + 1, 1) || substr(upper(hex(randomblob(2))),2) || '-' || upper(hex(randomblob(6)))";

            // 4. Migrate Data using Mapping Tables
            // A. Create a temporary map for ModPacks: OldIntID -> NewGuidID
            migrationBuilder.Sql("CREATE TABLE _Map_ModPack_Fs22 (OldId INTEGER, NewId TEXT);");
            migrationBuilder.Sql($"INSERT INTO _Map_ModPack_Fs22 SELECT Id, ({guidSql}) FROM ModPacks;");

            // B. Insert into New ModPacks table
            migrationBuilder.Sql(@"INSERT INTO ModPacks_New (Id, Author, CreatedAt, Description, Game, Guid, ImageSource, LastModifiedAt, Tags, Title, Version) 
        SELECT m.NewId, p.Author, p.CreatedAt, p.Description, p.Game, p.Guid, p.ImageSource, p.LastModifiedAt, p.Tags, p.Title, p.Version 
        FROM ModPacks p JOIN _Map_ModPack_Fs22 m ON p.Id = m.OldId;");

            // C. Insert into New Mods table (Mapping the FK ModPackId via the map)
            migrationBuilder.Sql(@"INSERT INTO Mods_New (Id, Author, CreatedAt, Description, FileName, ImageSource, IsMultiplayerCompatible, LastModifiedAt, ModPackId, Title, Version) 
        SELECT (" + guidSql + @"), s.Author, s.CreatedAt, s.Description, s.FileName, s.ImageSource, s.IsMultiplayerCompatible, s.LastModifiedAt, m.NewId, s.Title, s.Version
        FROM Mods s
        JOIN _Map_ModPack_Fs22 m ON s.ModPackId = m.OldId;");

            // 5. Swap Tables
            migrationBuilder.Sql("DROP TABLE Mods;");
            migrationBuilder.Sql("DROP TABLE ModPacks;");
            migrationBuilder.Sql("DROP TABLE _Map_ModPack_Fs22;");

            migrationBuilder.Sql("ALTER TABLE ModPacks_New RENAME TO ModPacks;");
            migrationBuilder.Sql("ALTER TABLE Mods_New RENAME TO Mods;");

            // 6. Re-create Indices
            migrationBuilder.Sql("CREATE INDEX IX_Mods_ModPackId ON Mods (ModPackId);");

            // 7. Re-enable Foreign Keys
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new InvalidOperationException();
        }
    }
}
