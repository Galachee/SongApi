namespace SongApi.Models;

public class Artist
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
    public IList<Song>? Songs { get; set; }
}