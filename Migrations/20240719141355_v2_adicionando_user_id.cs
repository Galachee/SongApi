using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SongApi.Migrations
{
    /// <inheritdoc />
    public partial class v2_adicionando_user_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "Playlist",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_PlaylistId",
                table: "Playlist",
                newName: "IX_Playlist_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Playlist",
                newName: "PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_UserId",
                table: "Playlist",
                newName: "IX_Playlist_PlaylistId");
        }
    }
}
