using SongApi.Enums;

namespace SongApi.Models;

public class Playlist
{
    
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;
    
    public IList<Song> Songs { get; set; } = null!;
    
    public User User { get; set; } = null!;
    
    public EGenre? Genre { get; set; } 

}