using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.Data.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "LeeftijdCategorie",
                columns: table => new
                {
                    LeeftijdCategorieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeeftijdCategorie", x => x.LeeftijdCategorieID);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    PlatformID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.PlatformID);
                });

            migrationBuilder.CreateTable(
                name: "SpelModus",
                columns: table => new
                {
                    SpelModusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpelModus", x => x.SpelModusID);
                });

            migrationBuilder.CreateTable(
                name: "Taal",
                columns: table => new
                {
                    TaalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taal", x => x.TaalID);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    Afbeelding = table.Column<string>(nullable: true),
                    Beschrijving = table.Column<string>(nullable: true),
                    KorteBeschrijving = table.Column<string>(nullable: true),
                    Review = table.Column<string>(nullable: true),
                    ReleaseDatum = table.Column<DateTime>(nullable: true),
                    DigitalOnly = table.Column<bool>(nullable: false),
                    leeftijdCategorieID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Game_LeeftijdCategorie_leeftijdCategorieID",
                        column: x => x.leeftijdCategorieID,
                        principalTable: "LeeftijdCategorie",
                        principalColumn: "LeeftijdCategorieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameGenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => x.GameGenreID);
                    table.ForeignKey(
                        name: "FK_GameGenre_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamePlatformID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: false),
                    PlatformID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => x.GamePlatformID);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platform_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platform",
                        principalColumn: "PlatformID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSpelModus",
                columns: table => new
                {
                    GameSpelModusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: false),
                    SpelModusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSpelModus", x => x.GameSpelModusID);
                    table.ForeignKey(
                        name: "FK_GameSpelModus_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSpelModus_SpelModus_SpelModusID",
                        column: x => x.SpelModusID,
                        principalTable: "SpelModus",
                        principalColumn: "SpelModusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTaal",
                columns: table => new
                {
                    GameTaalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: false),
                    TaalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTaal", x => x.GameTaalID);
                    table.ForeignKey(
                        name: "FK_GameTaal_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTaal_Taal_TaalID",
                        column: x => x.TaalID,
                        principalTable: "Taal",
                        principalColumn: "TaalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_leeftijdCategorieID",
                table: "Game",
                column: "leeftijdCategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GameID",
                table: "GameGenre",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreID",
                table: "GameGenre",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_GameID",
                table: "GamePlatform",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformID",
                table: "GamePlatform",
                column: "PlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSpelModus_GameID",
                table: "GameSpelModus",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSpelModus_SpelModusID",
                table: "GameSpelModus",
                column: "SpelModusID");

            migrationBuilder.CreateIndex(
                name: "IX_GameTaal_GameID",
                table: "GameTaal",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameTaal_TaalID",
                table: "GameTaal",
                column: "TaalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "GameSpelModus");

            migrationBuilder.DropTable(
                name: "GameTaal");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "SpelModus");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Taal");

            migrationBuilder.DropTable(
                name: "LeeftijdCategorie");
        }
    }
}
