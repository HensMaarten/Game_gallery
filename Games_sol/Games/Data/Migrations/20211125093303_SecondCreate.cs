using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.Data.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameUser",
                columns: table => new
                {
                    GameUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    GameID = table.Column<int>(nullable: true),
                    Uitgespeeld = table.Column<bool>(nullable: false),
                    Owned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUser", x => x.GameUserID);
                    table.ForeignKey(
                        name: "FK_GameUser_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_GameID",
                table: "GameUser",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_UserId",
                table: "GameUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUser");
        }
    }
}
