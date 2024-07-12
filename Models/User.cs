namespace SongApi.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public IList<Playlist> Playlists { get; set; } = new List<Playlist>();

    public IList<Role> Roles { get; set; } = null!;
}