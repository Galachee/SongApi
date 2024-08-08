using SongApi.Enums;

namespace SongApi.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public EGenre Genre { get; set; }
    public decimal DurationInMinutes { get; set; }
    public Artist Artist { get; set; } = null!;
    public int ArtistId { get; set; }

    public List<Playlist> Playlists { get; set; } = new();
}